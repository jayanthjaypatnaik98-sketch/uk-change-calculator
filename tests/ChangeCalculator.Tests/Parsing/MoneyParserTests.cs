using ChangeCalculator.Parsing;
using Xunit;

namespace ChangeCalculator.Tests.Parsing;

public class MoneyParserTests
{
    private readonly MoneyParser _parser = new();

    [Theory]
    [InlineData("20", 2000)]
    [InlineData("5.50", 550)]
    [InlineData("5.5", 550)]
    [InlineData("0", 0)]
    [InlineData("0.01", 1)]
    [InlineData("£5.50", 550)]
    [InlineData("  20  ", 2000)]
    public void TryParse_ValidInput_ReturnsExpectedPence(string input, long expectedPence)
    {
        var success = _parser.TryParse(input, out var pence);

        Assert.True(success);
        Assert.Equal(expectedPence, pence);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("-5")]
    [InlineData("5.5.5")]
    [InlineData(null)]
    public void TryParse_InvalidInput_ReturnsFalse(string? input)
    {
        var success = _parser.TryParse(input!, out var pence);

        Assert.False(success);
        Assert.Equal(0, pence);
    }
}
