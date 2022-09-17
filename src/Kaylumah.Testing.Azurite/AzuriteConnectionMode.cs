// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Represents the different connection modes to Azure Storage APIs.
/// </summary>
public enum AzuriteConnectionMode
{
    /// <summary>
    ///     Connect via ConnectionString.
    /// </summary>
    ConnectionString,

    /// <summary>
    ///     Connect via KeyCredential.
    /// </summary>
    SharedKeyCredential,

    /// <summary>
    ///     Connect via TokenCredential.
    /// </summary>
    TokenCredential,

    /// <summary>
    ///     Connect via SasCredential.
    /// </summary>
    AzureSasCredential
}
