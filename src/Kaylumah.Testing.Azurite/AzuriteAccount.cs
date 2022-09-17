// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text;

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     The <see cref="AzuriteAccount" /> provides access to Azure Storage API endpoints.
/// </summary>
public sealed class AzuriteAccount
{
    private string? _blobEndpoint;
    private string? _connectionString;
    private string? _queueEndpoint;
    private string? _tableEndpoint;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AzuriteAccount" /> class.
    /// </summary>
    /// <param name="accountName">The Storage Account name.</param>
    /// <param name="accountKey">The Storage Account access key.</param>
    /// <param name="defaultEndpointsProtocol">The protocol to use in Azure Storage API endpoints (default HTTP).</param>
    public AzuriteAccount(string accountName, string accountKey, string defaultEndpointsProtocol = "http")
    {
        AccountName = accountName;
        AccountKey = accountKey;
        DefaultEndpointsProtocol = defaultEndpointsProtocol;
    }

    private string BaseConnectionString =>
        $"DefaultEndpointsProtocol={DefaultEndpointsProtocol};AccountName={AccountName};AccountKey={AccountKey}";

    /// <summary>
    ///     The protocol <see cref="AzuriteAccount" /> uses to construct Azure Storage API endpoints.
    /// </summary>
    public string DefaultEndpointsProtocol { get; }

    /// <summary>
    ///     The account name <see cref="AzuriteAccount" /> uses to construct Azure Storage API endpoints.
    /// </summary>
    public string AccountName { get; }

    /// <summary>
    ///     The account key <see cref="AzuriteAccount" /> uses to construct Azure Storage API endpoints.
    /// </summary>
    public string AccountKey { get; }

    /// <summary>
    ///     The host address <see cref="AzuriteAccount" /> uses to construct the <see cref="BlobEndpoint" />.
    /// </summary>
    public string? BlobHost { get; set; }

    /// <summary>
    ///     The host port <see cref="AzuriteAccount" /> uses to construct the <see cref="BlobEndpoint" />.
    /// </summary>
    public int? BlobPort { get; set; }

    /// <summary>
    ///     The host address <see cref="AzuriteAccount" /> uses to construct the <see cref="QueueEndpoint" />.
    /// </summary>
    public string? QueueHost { get; set; }

    /// <summary>
    ///     The host port <see cref="AzuriteAccount" /> uses to construct the <see cref="QueueEndpoint" />.
    /// </summary>
    public int? QueuePort { get; set; }

    /// <summary>
    ///     The host address <see cref="AzuriteAccount" /> uses to construct the <see cref="TableEndpoint" />.
    /// </summary>
    public string? TableHost { get; set; }

    /// <summary>
    ///     The host port <see cref="AzuriteAccount" /> uses to construct the <see cref="TableEndpoint" />.
    /// </summary>
    public int? TablePort { get; set; }

    /// <summary>
    ///     Provides endpoint to use with Azure Blob Services.
    /// </summary>
    public string? BlobEndpoint => _blobEndpoint ??= CreateBlobEndpoint();

    /// <summary>
    ///     Provides endpoint to use with Azure Queue Services.
    /// </summary>
    public string? QueueEndpoint => _queueEndpoint ??= CreateQueueEndpoint();

    /// <summary>
    ///     Provides endpoint to use with Azure Table Services.
    /// </summary>
    public string? TableEndpoint => _tableEndpoint ??= CreateTableEndpoint();

    /// <summary>
    ///     Provides ConnectionString for all the configured Azure Storage Services.
    /// </summary>
    public string ConnectionString => _connectionString ??= CreateConnectionString();

    private string? CreateBlobEndpoint()
    {
        return !string.IsNullOrEmpty(BlobHost) && BlobPort is > 0
            ? $"{DefaultEndpointsProtocol}://{BlobHost}:{BlobPort}/{AccountName}"
            : null;
    }

    private string? CreateQueueEndpoint()
    {
        return !string.IsNullOrEmpty(QueueHost) && QueuePort is > 0
            ? $"{DefaultEndpointsProtocol}://{QueueHost}:{QueuePort}/{AccountName}"
            : null;
    }

    private string? CreateTableEndpoint()
    {
        return !string.IsNullOrEmpty(TableHost) && TablePort is > 0
            ? $"{DefaultEndpointsProtocol}://{TableHost}:{TablePort}/{AccountName}"
            : null;
    }

    private string CreateConnectionString()
    {
        var sb = new StringBuilder();
        sb.Append(BaseConnectionString);
        if (!string.IsNullOrEmpty(BlobEndpoint))
        {
            sb.Append(";BlobEndpoint=").Append(BlobEndpoint);
        }

        if (!string.IsNullOrEmpty(QueueEndpoint))
        {
            sb.Append(";QueueEndpoint=").Append(QueueEndpoint);
        }

        if (!string.IsNullOrEmpty(TableEndpoint))
        {
            sb.Append(";TableEndpoint=").Append(TableEndpoint);
        }

        sb.Append(';');

        return sb.ToString();
    }
}
