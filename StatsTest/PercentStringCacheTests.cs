﻿using FluentAssertions;
using Stats;
using System;
using Xunit;

namespace StatsTest;

public sealed class PercentStringCacheTests
{
    [Theory]
    [InlineData(10, 10)]
    [InlineData(10, -10)]
    [InlineData(-11, -10)]
    public void PercentStringCacheCtor_Should_ThrowException_ForInvalidInput(int minValue, int maxValue)
    {
        FluentActions
            .Invoking(() => new PercentStringCache(minValue, maxValue))
            .Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(-10, 10, null, "-%")]
    [InlineData(-10, 10, 0, "0%")]
    [InlineData(-10, 10, 1, "1%")]
    [InlineData(-10, 10, -1, "-1%")]
    [InlineData(-10, 10, 9, "9%")]
    [InlineData(-10, 10, -9, "-9%")]
    [InlineData(-10, 10, 10, "10%")]
    [InlineData(-10, 10, -10, "-10%")]
    [InlineData(-10, 10, 100, ">10%")]
    [InlineData(-10, 10, -100, "<-10%")]
    [InlineData(0, 999, -1, "<0%")]
    [InlineData(0, 999, 0, "0%")]
    [InlineData(0, 999, 1, "1%")]
    [InlineData(0, 999, 500, "500%")]
    [InlineData(0, 999, 999, "999%")]
    [InlineData(0, 999, 1000, ">999%")]
    public void GetPercentString_Should_ReturnString(int minValue, int maxValue, int? value, string expected)
    {
        var sut = new PercentStringCache(minValue, maxValue);
        var result = sut.GetPercentString(value);
        result.Should().Be(expected);
    }
}
