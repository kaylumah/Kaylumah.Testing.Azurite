# Kaylumah.Testing.Azurite

This package aims to provide a set of test utility classes for applications using
the [Azure SDK for dotnet](https://github.com/Azure/azure-sdk-for-net). Specifically, it provides access to Azure
Storage APIs via [Azurite](https://github.com/Azure/Azurite).

## Description

Azurite emulates Azure Blob, Azure Table and Azure Queue storage APIs. It can be run via NPM or as a Docker container.
Azurite has a lot of configuration options, so we cannot assume how it is running.

The package offers an `AzuriteAccount` class that provides access to the connection information. For example, if you
started Azurite without any options, you can create an AzuriteAccount like this:

```csharp
[Fact]
public void TestDefaultHttpConnectionString()
{
    AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder();
    account.ConnectionString
        .Should()
        .BeEquivalentTo(
            "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
}
```

For convenience, the package offers methods to create SDK clients.

For [Table Storage](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/tables/Azure.Data.Tables)

```csharp
[Fact(Timeout = 300_000)]
public async Task TestCanConnectToTableServiceViaConnectionString()
{
    AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder();
    var tableServiceClient = account.CreateTableServiceClient(AzuriteConnectionMode.ConnectionString);
    var properties = await tableServiceClient.GetPropertiesAsync();
    properties.GetRawResponse().Status.Should().Be(200);
}
```

For [Blob Storage](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage/Azure.Storage.Blobs)

```csharp
[Fact(Timeout = 300_000)]
public async Task TestCanConnectToBlobServiceViaConnectionString()
{
    AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder();
    var blobServiceClient = account.CreateBlobServiceClient(AzuriteConnectionMode.ConnectionString);
    var properties = await blobServiceClient.GetPropertiesAsync();
    properties.GetRawResponse().Status.Should().Be(200);
}
```

For [Queue Storage](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage/Azure.Storage.Queues)

```csharp
[Fact(Timeout = 300_000)]
public async Task TestCanConnectToQueueServiceViaConnectionString()
{
    AzuriteAccount account = AzuriteHelper.CreateDefaultAzuriteAccountBuilder();
    var queueServiceClient = account.CreateQueueServiceClient(AzuriteConnectionMode.ConnectionString);
    var properties = await queueServiceClient.GetPropertiesAsync();
    properties.GetRawResponse().Status.Should().Be(200);
}
```

## Features

- Does not assume how Azurite is running
    - Configure an `AzuriteAccountBuilder` to create an `AzuriteAccount`
- Create SDK clients from `AzuriteAccount`
    - Supports `BlobServiceClient`
    - Supports `TableServiceClient`
    - Supports `QueueServiceClient`
- Create SDK clients using
    - `ConnectionString`
    - `SharedKeyCredential`
    - `TokenCredential`
    - `AzureSasCredential`

## License

This repo is licensed under the [MIT License](LICENSE)