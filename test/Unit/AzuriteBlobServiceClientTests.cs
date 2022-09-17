// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.Testing.Azurite;
using Kaylumah.Testing.Azurite.Common;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollectionFixture.Name)]
public class AzuriteBlobServiceClientTests : IAsyncLifetime
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
    public async Task TestCanConnectToBlobServiceViaConnectionString()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var blobServiceClient = account.CreateBlobServiceClient(AzuriteConnectionMode.ConnectionString);
        var properties = await blobServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToBlobServiceViaTokenCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var blobServiceClient = account.CreateBlobServiceClient(AzuriteConnectionMode.TokenCredential);
        var properties = await blobServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToBlobServiceViaAzureSasCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var blobServiceClient = account.CreateBlobServiceClient(AzuriteConnectionMode.AzureSasCredential);
        var properties = await blobServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }

    [Fact(Timeout = 300_000)]
    public async Task TestCanConnectToBlobServiceViaSharedKeyCredential()
    {
        var account = _azuriteDockerRunner.GetAccount();
        var blobServiceClient = account.CreateBlobServiceClient(AzuriteConnectionMode.SharedKeyCredential);
        var properties = await blobServiceClient.GetPropertiesAsync();
        properties.GetRawResponse().Status.Should().Be(200);
    }
}
