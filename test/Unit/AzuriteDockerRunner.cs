// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Kaylumah.Testing.Azurite;

// ReSharper disable once CheckNamespace
namespace Test.Unit;

public class AzuriteDockerRunner
{
    private const string DockerImage = "mcr.microsoft.com/azure-storage/azurite";
    private const string DockerHost = "0.0.0.0";
    private const string DockerDirectory = "/data";
    private const string DockerContainerAzuriteLogFile = "debug.log";
    private const string DockerContainerAzuriteCertFile = "127.0.0.1.pem";
    private const string DockerContainerAzuriteKeyFile = "127.0.0.1-key.pem";
    private const string DockerAzuriteEnvVariableName = "AZURITE_ACCOUNTS";
    private const string DockerAzuriteEnvVariableValue = "account1:key1";

    private readonly IContainer _testContainer;

    private AzuriteAccountBuilder? _accountBuilder;

    public AzuriteDockerRunner()
    {
        var hostWorkingDirectory = Path.Combine(Environment.CurrentDirectory, "azurite");

        var arguments = new List<string>
        {
            "azurite",
            "--location",
            DockerDirectory,
            "--blobHost",
            DockerHost,
            "--queueHost",
            DockerHost,
            "--tableHost",
            DockerHost,
            "--silent",
            "--debug",
            $"{DockerDirectory}/{DockerContainerAzuriteLogFile}",
            "--cert",
            $"{DockerDirectory}/{DockerContainerAzuriteCertFile}",
            "--key",
            $"{DockerDirectory}/{DockerContainerAzuriteKeyFile}",
            "--oauth",
            "basic"
        }.ToArray();

        _testContainer
            = new ContainerBuilder()
                .WithImage(DockerImage)
                .WithBindMount(hostWorkingDirectory, DockerDirectory)
                .WithCommand(arguments)
                .WithEnvironment(DockerAzuriteEnvVariableName, DockerAzuriteEnvVariableValue)
                .WithPortBinding(AzuriteConstants.BlobServices.Port, true)
                .WithPortBinding(AzuriteConstants.QueueServices.Port, true)
                .WithPortBinding(AzuriteConstants.TableServices.Port, true)
                .WithWaitStrategy(
                    Wait.ForUnixContainer()
                        .UntilPortIsAvailable(AzuriteConstants.BlobServices.Port)
                        .UntilPortIsAvailable(AzuriteConstants.QueueServices.Port)
                        .UntilPortIsAvailable(AzuriteConstants.TableServices.Port)
                )
                .Build();
    }

    public AzuriteAccount GetAccount()
    {
        if (_accountBuilder == null)
        {
            throw new InvalidOperationException($"Ensure {nameof(InitializeAsync)} is called first!");
        }

        return _accountBuilder;
    }

    public async Task InitializeAsync()
    {
        await _testContainer.StartAsync();

        var blobPort = _testContainer.GetMappedPublicPort(AzuriteConstants.BlobServices.Port);
        var queuePort = _testContainer.GetMappedPublicPort(AzuriteConstants.QueueServices.Port);
        var tablePort = _testContainer.GetMappedPublicPort(AzuriteConstants.TableServices.Port);

        var accountParts = DockerAzuriteEnvVariableValue.Split(":");

        _accountBuilder = new AzuriteAccountBuilder()
            .WithProtocol(true)
            .WithBlobServices(AzuriteConstants.BlobServices.Host, blobPort)
            .WithQueueServices(AzuriteConstants.QueueServices.Host, queuePort)
            .WithTableServices(AzuriteConstants.TableServices.Host, tablePort)
            .WithAccount(accountParts[0], accountParts[1]);
    }

    public async Task DisposeAsync()
    {
        await _testContainer.DisposeAsync();
    }
}
