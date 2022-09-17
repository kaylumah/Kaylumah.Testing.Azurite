// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure.Storage.Queues;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="QueueServiceClient" />.
/// </summary>
public static class QueueServiceClientAzuriteAccountExtensions
{
    private static Uri ToQueueServiceClientEndpointUri(this AzuriteAccount azuriteAccount)
    {
        ArgumentNullException.ThrowIfNull(azuriteAccount);
        ArgumentNullException.ThrowIfNull(azuriteAccount.QueueEndpoint);
        return new Uri(azuriteAccount.QueueEndpoint);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Queues.QueueServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <param name="connectionMode">
    ///     A <see cref="AzuriteConnectionMode" /> to toggle the connection mode.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Queues.QueueServiceClient" /> for the azurite account.
    /// </returns>
    public static QueueServiceClient CreateQueueServiceClient(this AzuriteAccount account,
        AzuriteConnectionMode connectionMode)
    {
        ArgumentNullException.ThrowIfNull(account);
        var client = connectionMode switch
        {
            AzuriteConnectionMode.ConnectionString => account.CreateQueueServiceViaConnectionString(),
            AzuriteConnectionMode.SharedKeyCredential => account.CreateQueueServiceViaSharedKeyCredential(),
            AzuriteConnectionMode.TokenCredential => account.CreateQueueServiceViaTokenCredential(),
            AzuriteConnectionMode.AzureSasCredential => account.CreateQueueServiceViaAzureSasCredential(),
            _ => throw new ArgumentOutOfRangeException(nameof(connectionMode), connectionMode,
                "unsupported connection mode")
        };
        return client;
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Queues.QueueServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Queues.QueueServiceClient" /> for the azurite account.
    /// </returns>
    public static QueueServiceClient CreateQueueServiceViaConnectionString(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var options = AzuriteHelper.CreateQueueClientOptions();
        return new QueueServiceClient(account.ConnectionString, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Queues.QueueServiceClient" /> object via StorageSharedKeyCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Queues.QueueServiceClient" /> for the azurite account.
    /// </returns>
    public static QueueServiceClient CreateQueueServiceViaSharedKeyCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToQueueServiceClientEndpointUri();
        var credential = account.CreateStorageSharedKeyCredential();
        var options = AzuriteHelper.CreateQueueClientOptions();
        return new QueueServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Queues.QueueServiceClient" /> object via TokenCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Queues.QueueServiceClient" /> for the azurite account.
    /// </returns>
    public static QueueServiceClient CreateQueueServiceViaTokenCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToQueueServiceClientEndpointUri();
        var credential = AzuriteHelper.CreateTokenCredential();
        var options = AzuriteHelper.CreateQueueClientOptions();
        return new QueueServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Queues.QueueServiceClient" /> object via AzureSasCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Queues.QueueServiceClient" /> for the azurite account.
    /// </returns>
    public static QueueServiceClient CreateQueueServiceViaAzureSasCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToQueueServiceClientEndpointUri();
        var credential = account.CreateAzureSasCredential();
        var options = AzuriteHelper.CreateQueueClientOptions();
        return new QueueServiceClient(serviceUri, credential, options);
    }
}
