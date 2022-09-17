// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure.Storage.Blobs;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite.Common;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="BlobServiceClient" />.
/// </summary>
public static class AzuriteAccountBlobServiceClientExtensions
{
    private static Uri ToBlobServiceClientEndpointUri(this AzuriteAccount azuriteAccount)
    {
        ArgumentNullException.ThrowIfNull(azuriteAccount);
        ArgumentNullException.ThrowIfNull(azuriteAccount.BlobEndpoint);
        return new Uri(azuriteAccount.BlobEndpoint);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Blobs.BlobServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <param name="connectionMode">
    ///     A <see cref="AzuriteConnectionMode" /> to toggle the connection mode.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Blobs.BlobServiceClient" /> for the azurite account.
    /// </returns>
    public static BlobServiceClient CreateBlobServiceClient(this AzuriteAccount account,
        AzuriteConnectionMode connectionMode)
    {
        ArgumentNullException.ThrowIfNull(account);
        var client = connectionMode switch
        {
            AzuriteConnectionMode.ConnectionString => account.CreateBlobServiceViaConnectionString(),
            AzuriteConnectionMode.SharedKeyCredential => account.CreateBlobServiceViaSharedKeyCredential(),
            AzuriteConnectionMode.TokenCredential => account.CreateBlobServiceViaTokenCredential(),
            AzuriteConnectionMode.AzureSasCredential => account.CreateBlobServiceViaAzureSasCredential(),
            _ => throw new ArgumentOutOfRangeException(nameof(connectionMode), connectionMode,
                "unsupported connection mode")
        };
        return client;
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Blobs.BlobServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Blobs.BlobServiceClient" /> for the azurite account.
    /// </returns>
    public static BlobServiceClient CreateBlobServiceViaConnectionString(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var options = AzuriteHelper.CreateBlobClientOptions();
        return new BlobServiceClient(account.ConnectionString, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Blobs.BlobServiceClient" /> object via StorageSharedKeyCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Blobs.BlobServiceClient" /> for the azurite account.
    /// </returns>
    public static BlobServiceClient CreateBlobServiceViaSharedKeyCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToBlobServiceClientEndpointUri();
        var credential = account.CreateStorageSharedKeyCredential();
        var options = AzuriteHelper.CreateBlobClientOptions();
        return new BlobServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Blobs.BlobServiceClient" /> object via TokenCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Blobs.BlobServiceClient" /> for the azurite account.
    /// </returns>
    public static BlobServiceClient CreateBlobServiceViaTokenCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToBlobServiceClientEndpointUri();
        var credential = AzuriteHelper.CreateTokenCredential();
        var options = AzuriteHelper.CreateBlobClientOptions();
        return new BlobServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Storage.Blobs.BlobServiceClient" /> object via AzureSasCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Storage.Blobs.BlobServiceClient" /> for the azurite account.
    /// </returns>
    public static BlobServiceClient CreateBlobServiceViaAzureSasCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToBlobServiceClientEndpointUri();
        var credential = account.CreateAzureSasCredential();
        var options = AzuriteHelper.CreateBlobClientOptions();
        return new BlobServiceClient(serviceUri, credential, options);
    }
}
