// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Kaylumah.Testing.Azurite;

/// <summary>
///     Provides a credential for azure storage.
/// </summary>
public sealed class AzuriteTokenCredential : TokenCredential
{
    /// <inheritdoc />
    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext,
        CancellationToken cancellationToken)
    {
        return new ValueTask<AccessToken>(GetToken(requestContext, cancellationToken));
    }

    /// <inheritdoc />
    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        //{
        // "aud": "https://storage.azure.com",
        // "iss": "https://sts.windows-ppe.net/ab1f708d-50f6-404c-a006-d71b2ac7a606/",
        // "iat": 1511859603,
        // "nbf": 1511859603,
        // "exp": 9999999999,
        // "alg": "HS256"
        //}
        // Encoded using https://jwt.io/
        return new AccessToken(
            "eyJhdWQiOiJodHRwczovL3N0b3JhZ2UuYXp1cmUuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy1wcGUubmV0L2FiMWY3MDhkLTUwZjYtNDA0Yy1hMDA2LWQ3MWIyYWM3YTYwNi8iLCJpYXQiOjE1MTE4NTk2MDMsIm5iZiI6MTUxMTg1OTYwMywiZXhwIjo5OTk5OTk5OTk5LCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJodHRwczovL3N0b3JhZ2UuYXp1cmUuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy1wcGUubmV0L2FiMWY3MDhkLTUwZjYtNDA0Yy1hMDA2LWQ3MWIyYWM3YTYwNi8iLCJpYXQiOjE1MTE4NTk2MDMsIm5iZiI6MTUxMTg1OTYwMywiZXhwIjo5OTk5OTk5OTk5LCJhbGciOiJIUzI1NiJ9.z48ZJz_3k0ZOATIMjZ02AQxlDnUT3NXLEJXLgdHIKl8",
            DateTimeOffset.MaxValue);
    }
}
