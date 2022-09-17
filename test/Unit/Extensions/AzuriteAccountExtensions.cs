// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Kaylumah.Testing.Azurite;

public static class AzuriteAccountExtensions
{
    private const string PartSeparator = ";";
    private const string ValueSeparator = "=";

    public static Dictionary<string, string> ToConnectionStringData(this AzuriteAccount account)
    {
        var connectionString = account.ConnectionString;
        var connectionStringParts = connectionString.Split(PartSeparator);
        return connectionStringParts
            .Where(part => part.Contains(ValueSeparator))
            .Select(part => part.Split(ValueSeparator))
            .ToDictionary(item => item[0], item => item[1]);
    }
}
