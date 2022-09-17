// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Extends <see cref="AzuriteAccountBuilder" /> with convenience methods.
/// </summary>
public static class AzuriteAccountBuilderExtensions
{
    /// <summary>
    ///     Configures an <see cref="AzuriteAccountBuilder" /> to use a random account.
    /// </summary>
    /// <param name="azuriteAccountBuilder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns></returns>
    public static AzuriteAccountBuilder WithRandomAccount(this AzuriteAccountBuilder azuriteAccountBuilder)
    {
        var name = Guid.NewGuid().ToString();
        var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        return azuriteAccountBuilder.WithAccount(name, key);
    }

    /// <summary>
    ///     Configures an <see cref="AzuriteAccountBuilder" /> to use the default azurite account.
    /// </summary>
    /// <param name="azuriteAccountBuilder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns></returns>
    public static AzuriteAccountBuilder WithDefaultAccount(this AzuriteAccountBuilder azuriteAccountBuilder)
    {
        return azuriteAccountBuilder.WithAccount(AzuriteConstants.Account.AccountName,
            AzuriteConstants.Account.AccountKey);
    }

    /// <summary>
    ///     Configures an <see cref="AzuriteAccountBuilder" /> to use the default blob endpoint.
    /// </summary>
    /// <param name="azuriteAccountBuilder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns></returns>
    public static AzuriteAccountBuilder WithDefaultBlobEndpoint(this AzuriteAccountBuilder azuriteAccountBuilder)
    {
        return azuriteAccountBuilder.WithBlobServices(AzuriteConstants.BlobServices.Host,
            AzuriteConstants.BlobServices.Port);
    }

    /// <summary>
    ///     Configures an <see cref="AzuriteAccountBuilder" /> to use the default queue endpoint.
    /// </summary>
    /// <param name="azuriteAccountBuilder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns></returns>
    public static AzuriteAccountBuilder WithDefaultQueueEndpoint(this AzuriteAccountBuilder azuriteAccountBuilder)
    {
        return azuriteAccountBuilder.WithQueueServices(AzuriteConstants.QueueServices.Host,
            AzuriteConstants.QueueServices.Port);
    }

    /// <summary>
    ///     Configures an <see cref="AzuriteAccountBuilder" /> to use the default table endpoint.
    /// </summary>
    /// <param name="azuriteAccountBuilder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns></returns>
    public static AzuriteAccountBuilder WithDefaultTableEndpoint(this AzuriteAccountBuilder azuriteAccountBuilder)
    {
        return azuriteAccountBuilder.WithTableServices(AzuriteConstants.TableServices.Host,
            AzuriteConstants.TableServices.Port);
    }
}
