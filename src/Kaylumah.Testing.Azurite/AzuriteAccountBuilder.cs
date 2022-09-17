// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     The <see cref="AzuriteAccountBuilder" /> provides methods to construct an <see cref="AzuriteAccount" />.
/// </summary>
public class AzuriteAccountBuilder
{
    private string? _accountKey;
    private string? _accountName;
    private string? _blobHost;
    private int? _blobPort;
    private string? _defaultEndpointsProtocol;
    private string? _queueHost;
    private int? _queuePort;
    private string? _tableHost;
    private int? _tablePort;

    /// <summary>
    ///     Configures the endpoints protocol.
    /// </summary>
    /// <param name="secure">Configure http or https (default: http).</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> being configured.</returns>
    public AzuriteAccountBuilder WithProtocol(bool secure = false)
    {
        _defaultEndpointsProtocol = secure ? AzuriteConstants.Protocols.Secure : AzuriteConstants.Protocols.Unsecure;
        return this;
    }

    /// <summary>
    ///     Configures the account.
    /// </summary>
    /// <param name="accountName">The account name.</param>
    /// <param name="accountKey">The account key.</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> being configured.</returns>
    public AzuriteAccountBuilder WithAccount(string accountName, string accountKey)
    {
        _accountName = accountName;
        _accountKey = accountKey;
        return this;
    }

    /// <summary>
    ///     Configures the BlobServices endpoint.
    /// </summary>
    /// <param name="blobHost">the blob host address.</param>
    /// <param name="blobPort">the blob host port.</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> being configured.</returns>
    public AzuriteAccountBuilder WithBlobServices(string blobHost, int blobPort)
    {
        _blobHost = blobHost;
        _blobPort = blobPort;
        return this;
    }

    /// <summary>
    ///     Configures the TableServices endpoint.
    /// </summary>
    /// <param name="tableHost">the table host address.</param>
    /// <param name="tablePort">the table host port.</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> being configured.</returns>
    public AzuriteAccountBuilder WithTableServices(string tableHost, int tablePort)
    {
        _tableHost = tableHost;
        _tablePort = tablePort;
        return this;
    }

    /// <summary>
    ///     Configures the QueueServices endpoint.
    /// </summary>
    /// <param name="queueHost">the queue host address.</param>
    /// <param name="queuePort">the queue host port.</param>
    /// <returns>The <see cref="AzuriteAccountBuilder" /> being configured.</returns>
    public AzuriteAccountBuilder WithQueueServices(string queueHost, int queuePort)
    {
        _queueHost = queueHost;
        _queuePort = queuePort;
        return this;
    }

    /// <summary>
    ///     Constructs a new <see cref="AzuriteAccount" />.
    /// </summary>
    /// <returns>An <see cref="AzuriteAccount" />.</returns>
    public AzuriteAccount Build()
    {
        ArgumentNullException.ThrowIfNull(_defaultEndpointsProtocol);
        ArgumentNullException.ThrowIfNull(_accountName);
        ArgumentNullException.ThrowIfNull(_accountKey);

        var account = new AzuriteAccount(_accountName, _accountKey, _defaultEndpointsProtocol);

        if (!string.IsNullOrEmpty(_blobHost) && _blobPort is > 0)
        {
            account.BlobHost = _blobHost;
            account.BlobPort = _blobPort;
        }

        if (!string.IsNullOrEmpty(_queueHost) && _queuePort is > 0)
        {
            account.QueueHost = _queueHost;
            account.QueuePort = _queuePort;
        }

        if (!string.IsNullOrEmpty(_tableHost) && _tablePort is > 0)
        {
            account.TableHost = _tableHost;
            account.TablePort = _tablePort;
        }

        return account;
    }

    /// <summary>
    ///     Constructs a new <see cref="AzuriteAccount" />.
    /// </summary>
    /// <param name="builder">The <see cref="AzuriteAccountBuilder" /> being configured.</param>
    /// <returns>An <see cref="AzuriteAccount" />.</returns>
    public static implicit operator AzuriteAccount(AzuriteAccountBuilder builder)
    {
        return builder.Build();
    }
}
