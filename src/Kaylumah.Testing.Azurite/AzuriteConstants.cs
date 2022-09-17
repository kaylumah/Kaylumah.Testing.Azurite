// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Provides constants for Azurite Defaults.
/// </summary>
public static class AzuriteConstants
{
    /// <summary>
    ///     Provides default endpoint protocols for Azurite.
    /// </summary>
    public struct Protocols
    {
        /// <summary>
        ///     Default unsecure option for DefaultEndpointsProtocol.
        /// </summary>
        public const string Unsecure = "http";

        /// <summary>
        ///     Secure option for DefaultEndpointsProtocol.
        /// </summary>
        public const string Secure = "https";
    }

    /// <summary>
    ///     Provides default account details for Azurite.
    /// </summary>
    public struct Account
    {
        /// <summary>
        ///     Default Storage Account AccountName.
        /// </summary>
        public const string AccountName = "devstoreaccount1";

        /// <summary>
        ///     Default Storage Account AccountKey.
        /// </summary>
        public const string AccountKey =
            "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
    }

    /// <summary>
    ///     Provides default blob service details for Azurite.
    /// </summary>
    public struct BlobServices
    {
        /// <summary>
        ///     Default Blob Service host address.
        /// </summary>
        public const string Host = "127.0.0.1";

        /// <summary>
        ///     Default Blob Service host port.
        /// </summary>
        public const int Port = 10000;
    }

    /// <summary>
    ///     Provides default queue service details for Azurite.
    /// </summary>
    public struct QueueServices
    {
        /// <summary>
        ///     Default Blob Service host address.
        /// </summary>
        public const string Host = "127.0.0.1";

        /// <summary>
        ///     Default Blob Service host port.
        /// </summary>
        public const int Port = 10001;
    }

    /// <summary>
    ///     Provides default table service details for Azurite.
    /// </summary>
    public struct TableServices
    {
        /// <summary>
        ///     Default Blob Service host address.
        /// </summary>
        public const string Host = "127.0.0.1";

        /// <summary>
        ///     Default Blob Service host port.
        /// </summary>
        public const int Port = 10002;
    }
}
