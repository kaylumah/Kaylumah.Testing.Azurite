// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using FluentAssertions;
using FluentAssertions.Execution;
using Kaylumah.Testing.Azurite;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollectionFixture.Name)]
public class AzuriteAccountBuilderTests
{
    [Fact]
    public void TestDefaultHttpConnectionString()
    {
        AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder();
        account.ConnectionString
            .Should()
            .BeEquivalentTo(
                "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
    }

    [Fact]
    public void TestDefaultHttpsConnectionString()
    {
        AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder(true);
        account.ConnectionString
            .Should()
            .BeEquivalentTo(
                "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=https://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=https://127.0.0.1:10001/devstoreaccount1;TableEndpoint=https://127.0.0.1:10002/devstoreaccount1;");
    }

    [Fact]
    public void TestDefaultHttpConnectionStringParts()
    {
        AzuriteAccount account = new AzuriteAccountBuilder()
            .WithProtocol()
            .WithDefaultAccount()
            .WithDefaultBlobEndpoint()
            .WithDefaultQueueEndpoint()
            .WithDefaultTableEndpoint();
        var connectionStringData = account.ToConnectionStringData();

        using var scope = new AssertionScope();

        connectionStringData
            .Should()
            .ContainKey("DefaultEndpointsProtocol")
            .WhoseValue
            .Should()
            .Be("http");
    }

    [Fact]
    public void TestDefaultHttpsConnectionStringParts()
    {
        AzuriteAccount account = new AzuriteAccountBuilder()
            .WithProtocol(true)
            .WithDefaultAccount()
            .WithDefaultBlobEndpoint()
            .WithDefaultQueueEndpoint()
            .WithDefaultTableEndpoint();
        var connectionStringData = account.ToConnectionStringData();

        using var scope = new AssertionScope();

        connectionStringData
            .Should()
            .ContainKey("DefaultEndpointsProtocol")
            .WhoseValue
            .Should()
            .Be("https");
    }
}
