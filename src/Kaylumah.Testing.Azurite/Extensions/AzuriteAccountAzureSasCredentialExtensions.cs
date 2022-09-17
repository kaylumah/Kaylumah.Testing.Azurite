// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure;
using Azure.Storage.Sas;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="AzureSasCredential" />.
/// </summary>
public static class AzuriteAccountAzureSasCredentialExtensions
{
    /// <summary>
    ///     Creates an <see cref="AzureSasCredential" />.
    /// </summary>
    /// <param name="account">The <see cref="AzuriteAccount" /> used.</param>
    /// <returns>An <see cref="AzureSasCredential" />.</returns>
    public static AzureSasCredential CreateAzureSasCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        var sas = new AccountSasBuilder
        {
            Services = AccountSasServices.All,
            ResourceTypes = AccountSasResourceTypes.All,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };
        sas.SetPermissions(AccountSasPermissions.All);

        var credential = account.CreateStorageSharedKeyCredential();
        var sasQuery = sas.ToSasQueryParameters(credential).ToString();
        var sasCredential = new AzureSasCredential(sasQuery);
        return sasCredential;
    }
}
