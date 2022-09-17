// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using Azure.Data.Tables;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccount" /> with option to create <see cref="TableSharedKeyCredential" />.
/// </summary>
public static class AzuriteAccountTableSharedKeyCredential
{
    /// <summary>
    ///     Creates an <see cref="TableSharedKeyCredential" />.
    /// </summary>
    /// <param name="account">The <see cref="AzuriteAccount" /> used.</param>
    /// <returns>An <see cref="TableSharedKeyCredential" />.</returns>
    public static TableSharedKeyCredential CreateTableSharedKeyCredential(this AzuriteAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        return new TableSharedKeyCredential(account.AccountName, account.AccountKey);
    }
}
