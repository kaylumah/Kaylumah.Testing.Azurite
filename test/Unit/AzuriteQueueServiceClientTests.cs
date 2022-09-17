// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.Testing.Azurite;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollectionFixture.Name)]
public class AzuriteQueueServiceClientTests : IAsyncLifetime
{
    private readonly AzuriteDockerRunner _azuriteDockerRunner = new();

    public async Task InitializeAsync()
    {
        await _azuriteDockerRunner.InitializeAsync().ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await _azuriteDockerRunner.DisposeAsync().ConfigureAwait(false);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToQueueServiceViaConnectionString()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var queueServiceClient = account.CreateQueueServiceClient(AzuriteConnectionMode.ConnectionString);
        var properties = await queueServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToQueueServiceViaTokenCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var queueServiceClient = account.CreateQueueServiceClient(AzuriteConnectionMode.TokenCredential);
        var properties = await queueServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToQueueServiceViaAzureSasCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var queueServiceClient = account.CreateQueueServiceClient(AzuriteConnectionMode.AzureSasCredential);
        var properties = await queueServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToQueueServiceViaSharedKeyCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var queueServiceClient = account.CreateQueueServiceClient(AzuriteConnectionMode.SharedKeyCredential);
        var properties = await queueServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }
}
