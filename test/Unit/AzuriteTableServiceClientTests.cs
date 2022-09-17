// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.Testing.Azurite;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollectionFixture.Name)]
public class AzuriteTableServiceClientTests : IAsyncLifetime
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
    public async Task TestCanConnectToTableServiceViaConnectionString()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var tableServiceClient = account.CreateTableServiceClient(AzuriteConnectionMode.ConnectionString);
        var properties = await tableServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToTableServiceViaTokenCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var tableServiceClient = account.CreateTableServiceClient(AzuriteConnectionMode.TokenCredential);
        var properties = await tableServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToTableServiceViaAzureSasCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var tableServiceClient = account.CreateTableServiceClient(AzuriteConnectionMode.AzureSasCredential);
        var properties = await tableServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToTableServiceViaSharedKeyCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var tableServiceClient = account.CreateTableServiceClient(AzuriteConnectionMode.SharedKeyCredential);
        var properties = await tableServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }
}
