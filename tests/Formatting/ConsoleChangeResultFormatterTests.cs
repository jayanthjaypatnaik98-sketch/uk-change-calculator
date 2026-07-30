using System.Linq;
using ChangeCalculator.Domain;
using ChangeCalculator.Formatting;
using Xunit;

namespace ChangeCalculator.Tests.Formatting;

public class ConsoleChangeResultFormatterTests
{
    private readonly ConsoleChangeResultFormatter _formatter = new();

    [Fact]
    public void Format_SuccessWithBreakdown_ListsEachLine()
    {
        var tenPound = Denomination.UkDenominations.Single(d => d.DisplayName == "£10");
        var twoPound = Denomination.UkDenominations.Single(d => d.DisplayName == "£2");

        var result = ChangeCalculationResult.Success(new List<ChangeBreakdownLine>
        {
            new(tenPound, 1),
            new(twoPound, 2),
        });

        var output = _formatter.Format(result);

        Assert.Contains("Your change is:", output);
        Assert.Contains("1 x £10", output);
        Assert.Contains("2 x £2", output);
    }

    [Fact]
    public void Format_SuccessWithNoChange_ReturnsNoChangeMessage()
    {
        var result = ChangeCalculationResult.Success(new List<ChangeBreakdownLine>());

        var output = _formatter.Format(result);

        Assert.Equal("Your change is: no change due.", output);
    }

    [Fact]
    public void Format_Failure_ReturnsErrorMessage()
    {
        var result = ChangeCalculationResult.Failure("Amount paid is less than the price.");

        var output = _formatter.Format(result);

        Assert.Equal("Error: Amount paid is less than the price.", output);
    }
}
