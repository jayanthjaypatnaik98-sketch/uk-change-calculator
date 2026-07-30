using ChangeCalculator.Services;
using Xunit;

namespace ChangeCalculator.Tests.Services;

public class ChangeBreakdownServiceTests
{
    private readonly ChangeBreakdownService _service = new();

    [Fact]
    public void Calculate_TaskExample_ReturnsExpectedBreakdown()
    {
        // £20 paid, £5.50 price -> change of £14.50
        var result = _service.Calculate(amountPaidInPence: 2000, priceInPence: 550);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Breakdown.Count);

        Assert.Equal("£10", result.Breakdown[0].Denomination.DisplayName);
        Assert.Equal(1, result.Breakdown[0].Count);

        Assert.Equal("£2", result.Breakdown[1].Denomination.DisplayName);
        Assert.Equal(2, result.Breakdown[1].Count);

        Assert.Equal("50p", result.Breakdown[2].Denomination.DisplayName);
        Assert.Equal(1, result.Breakdown[2].Count);
    }

    [Fact]
    public void Calculate_ExactPayment_ReturnsEmptyBreakdown()
    {
        var result = _service.Calculate(amountPaidInPence: 550, priceInPence: 550);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Breakdown);
    }

    [Fact]
    public void Calculate_AmountPaidLessThanPrice_ReturnsFailure()
    {
        var result = _service.Calculate(amountPaidInPence: 500, priceInPence: 550);

        Assert.False(result.IsSuccess);
        Assert.Equal("Amount paid is less than the price.", result.ErrorMessage);
    }

    [Theory]
    [InlineData(-100, 550)]
    [InlineData(2000, -550)]
    public void Calculate_NegativeAmounts_ReturnsFailure(long amountPaid, long price)
    {
        var result = _service.Calculate(amountPaid, price);

        Assert.False(result.IsSuccess);
        Assert.Equal("Amounts cannot be negative.", result.ErrorMessage);
    }

    [Fact]
    public void Calculate_LargeAmount_UsesLargestDenominationsFirst()
    {
        // £1000 paid, £0.01 price -> £999.99 change
        var result = _service.Calculate(amountPaidInPence: 100000, priceInPence: 1);

        Assert.True(result.IsSuccess);
        Assert.Equal("£50", result.Breakdown[0].Denomination.DisplayName);
        Assert.Equal(19, result.Breakdown[0].Count); // 19 x £50 = £950
    }

    [Fact]
    public void Calculate_OnePennyChange_ReturnsSinglePennyLine()
    {
        var result = _service.Calculate(amountPaidInPence: 551, priceInPence: 550);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Breakdown);
        Assert.Equal("1p", result.Breakdown[0].Denomination.DisplayName);
        Assert.Equal(1, result.Breakdown[0].Count);
    }
}
