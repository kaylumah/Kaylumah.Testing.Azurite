// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using Xunit;

namespace Test.Unit;

[CollectionDefinition(Name)]
public sealed class TestProjectCollectionFixture : ICollectionFixture<TestProjectFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = "Test.Unit";
}

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TestProjectFixture
{
}
