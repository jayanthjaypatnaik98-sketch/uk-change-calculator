using ChangeCalculator;
using Xunit;

namespace ChangeCalculator.Tests;

public class ChangeBreakdownServiceTests
{
    private readonly ChangeBreakdownService _service = new();

    [Fact]
    public void ExampleFromTask_20Paid_5_50Price_ReturnsExpectedBreakdown()
    {
        // Given £20 and a product price of £5.50, change due is £14.50:
        // 1 x £10, 2 x £2, 1 x 50p
        var result = _service.CalculateChange(20m, 5.50m);

        Assert.Equal(3, result.Count);
        Assert.Equal(new DenominationCount("£10", 1), result[0]);
        Assert.Equal(new DenominationCount("£2", 2), result[1]);
        Assert.Equal(new DenominationCount("50p", 1), result[2]);
    }

    [Fact]
    public void ExactAmount_ReturnsNoChange()
    {
        var result = _service.CalculateChange(5.50m, 5.50m);

        Assert.Empty(result);
    }

    [Fact]
    public void SmallCoins_AreBrokenDownCorrectly()
    {
        // £0.08 change should be 1 x 5p, 1 x 2p, 1 x 1p
        var result = _service.CalculateChange(1.00m, 0.92m);

        Assert.Equal(new DenominationCount("5p", 1), result[0]);
        Assert.Equal(new DenominationCount("2p", 1), result[1]);
        Assert.Equal(new DenominationCount("1p", 1), result[2]);
    }

    [Fact]
    public void AmountPaidLessThanPrice_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _service.CalculateChange(5m, 10m));
    }

    [Fact]
    public void LargeAmount_UsesHighestDenominationsFirst()
    {
        // £100 paid, price £0.01 -> £99.99 change, should start with 1 x £50
        var result = _service.CalculateChange(100m, 0.01m);

        Assert.Equal("£50", result[0].Label);
        Assert.Equal(1, result[0].Count);
    }
}
