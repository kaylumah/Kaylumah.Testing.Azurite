// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Net.Http;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Helper to interface with Azure Storage APIs with Azurite.
/// </summary>
public static class AzuriteHelper
{
    /// <summary>
    ///     Provides a <see cref="TokenCredential" /> to use with Azurite.
    /// </summary>
    /// <returns>An <see cref="AzuriteTokenCredential" />.</returns>
    public static TokenCredential CreateTokenCredential()
    {
        return new AzuriteTokenCredential();
    }

    /// <summary>
    ///     Provides <see cref="BlobClientOptions" /> configured to work with Azurite.
    /// </summary>
    /// <returns>A <see cref="BlobClientOptions" /> instance.</returns>
    public static BlobClientOptions CreateBlobClientOptions()
    {
        return new BlobClientOptions { Transport = CreateHttpClientTransport() };
    }

    /// <summary>
    ///     Provides <see cref="QueueClientOptions" /> configured to work with Azurite.
    /// </summary>
    /// <returns>A <see cref="QueueClientOptions" /> instance.</returns>
    public static QueueClientOptions CreateQueueClientOptions()
    {
        return new QueueClientOptions { Transport = CreateHttpClientTransport() };
    }

    /// <summary>
    ///     Provides <see cref="TableClientOptions" /> configured to work with Azurite.
    /// </summary>
    /// <returns>A <see cref="TableClientOptions" /> instance.</returns>
    public static TableClientOptions CreateTableClientOptions()
    {
        return new TableClientOptions { Transport = CreateHttpClientTransport() };
    }

    /// <summary>
    ///     Provides <see cref="AzuriteAccountBuilder" /> configured with the default Azurite settings.
    /// </summary>
    /// <param name="secure">Indicates the endpoints should be secure (default http).</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> configured with defaults.</returns>
    public static AzuriteAccountBuilder CreateDefaultAzuriteAccountBuilder(bool secure = false)
    {
        return new AzuriteAccountBuilder()
            .WithProtocol(secure)
            .WithDefaultAccount()
            .WithDefaultBlobEndpoint()
            .WithDefaultQueueEndpoint()
            .WithDefaultTableEndpoint();
    }

    private static HttpClientTransport CreateHttpClientTransport()
    {
        var transport = new HttpClientTransport(new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        }));
        return transport;
    }
}
