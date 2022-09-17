// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure.Storage;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="StorageSharedKeyCredential" />.
/// </summary>
public static class AzuriteAccountStorageSharedKeyCredentialExtensions
{
    /// <summary>
    ///     Creates an <see cref="StorageSharedKeyCredential" />.
    /// </summary>
    /// <param name="account">The <see cref="AzuriteAccount" /> used.</param>
    /// <returns>An <see cref="StorageSharedKeyCredential" />.</returns>
    public static StorageSharedKeyCredential CreateStorageSharedKeyCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        return new StorageSharedKeyCredential(account.AccountName, account.AccountKey);
    }
}
