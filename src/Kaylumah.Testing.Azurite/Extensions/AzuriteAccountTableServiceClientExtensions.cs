// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure.Data.Tables;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="TableServiceClient" />.
/// </summary>
public static class TableServiceClientAzuriteAccountExtensions
{
    private static Uri ToTableServiceClientEndpointUri(this AzuriteAccount azuriteAccount)
    {
        ArgumentNullException.ThrowIfNull(azuriteAccount);
        ArgumentNullException.ThrowIfNull(azuriteAccount.TableEndpoint);
        return new Uri(azuriteAccount.TableEndpoint);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Data.Tables.TableServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <param name="connectionMode">
    ///     A <see cref="AzuriteConnectionMode" /> to toggle the connection mode.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Data.Tables.TableServiceClient" /> for the azurite account.
    /// </returns>
    public static TableServiceClient CreateTableServiceClient(this AzuriteAccount account,
        AzuriteConnectionMode connectionMode)
    {
        ArgumentNullException.ThrowIfNull(account);
        var client = connectionMode switch
        {
            AzuriteConnectionMode.ConnectionString => account.CreateTableServiceViaConnectionString(),
            AzuriteConnectionMode.SharedKeyCredential => account.CreateTableServiceViaSharedKeyCredential(),
            AzuriteConnectionMode.TokenCredential => account.CreateTableServiceViaTokenCredential(),
            AzuriteConnectionMode.AzureSasCredential => account.CreateTableServiceViaAzureSasCredential(),
            _ => throw new ArgumentOutOfRangeException(nameof(connectionMode), connectionMode,
                "unsupported connection mode")
        };
        return client;
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Data.Tables.TableServiceClient" /> object via ConnectionString.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Data.Tables.TableServiceClient" /> for the azurite account.
    /// </returns>
    public static TableServiceClient CreateTableServiceViaConnectionString(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var options = AzuriteHelper.CreateTableClientOptions();
        return new TableServiceClient(account.ConnectionString, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Data.Tables.TableServiceClient" /> object via StorageSharedKeyCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Data.Tables.TableServiceClient" /> for the azurite account.
    /// </returns>
    public static TableServiceClient CreateTableServiceViaSharedKeyCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToTableServiceClientEndpointUri();
        var credential = account.CreateTableSharedKeyCredential();
        var options = AzuriteHelper.CreateTableClientOptions();
        return new TableServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Data.Tables.TableServiceClient" /> object via TokenCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Data.Tables.TableServiceClient" /> for the azurite account.
    /// </returns>
    public static TableServiceClient CreateTableServiceViaTokenCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToTableServiceClientEndpointUri();
        var credential = AzuriteHelper.CreateTokenCredential();
        var options = AzuriteHelper.CreateTableClientOptions();
        return new TableServiceClient(serviceUri, credential, options);
    }

    /// <summary>
    ///     Create a new <see cref="Azure.Data.Tables.TableServiceClient" /> object via AzureSasCredential.
    /// </summary>
    /// <param name="account">
    ///     An <see cref="AzuriteAccount" /> to create a BlobServiceClient.
    /// </param>
    /// <returns>
    ///     A <see cref="Azure.Data.Tables.TableServiceClient" /> for the azurite account.
    /// </returns>
    public static TableServiceClient CreateTableServiceViaAzureSasCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var serviceUri = account.ToTableServiceClientEndpointUri();
        var credential = account.CreateAzureSasCredential();
        var options = AzuriteHelper.CreateTableClientOptions();
        return new TableServiceClient(serviceUri, credential, options);
    }
}
