using System.Globalization;

namespace ChangeCalculator.Parsing;

public sealed class MoneyParser : IMoneyParser
{
    public bool TryParse(string input, out long amountInPence)
    {
        amountInPence = 0;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var trimmed = input.Trim().TrimStart('£');

        if (!decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out var pounds))
        {
            return false;
        }

        if (pounds < 0)
        {
            return false;
        }

        // Round to the nearest penny before converting to pence, so "5.5"
        // and "5.50" are treated identically and we never carry float error.
        amountInPence = (long)Math.Round(pounds * 100m, MidpointRounding.AwayFromZero);
        return true;
    }
}
