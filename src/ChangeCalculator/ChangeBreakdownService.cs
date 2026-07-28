namespace ChangeCalculator;

/// <summary>
/// Represents a quantity of a single UK coin/note denomination in the change breakdown.
/// </summary>
public record DenominationCount(string Label, int Count);

/// <summary>
/// Calculates the minimum number of UK coins/notes needed to make up a given
/// amount of change, largest denomination first (a classic "greedy" change-making
/// algorithm). This works correctly for the UK/US-style coin system because every
/// denomination divides evenly into the ones above it in the relevant way.
/// </summary>
public class ChangeBreakdownService
{
    // All amounts are handled in whole pence internally. Working in decimals/doubles
    // for money is a common source of rounding bugs (e.g. 0.1m + 0.2m style errors),
    // so we convert to an integer (pence) the moment we read the input and never
    // touch decimal math again.
    private static readonly (string Label, int Pence)[] Denominations =
    {
        ("£50", 5000),
        ("£20", 2000),
        ("£10", 1000),
        ("£5",   500),
        ("£2",   200),
        ("£1",   100),
        ("50p",   50),
        ("20p",   20),
        ("10p",   10),
        ("5p",     5),
        ("2p",     2),
        ("1p",     1),
    };

    /// <summary>
    /// Given the amount a customer paid and the price of the product (both in pounds),
    /// returns the change broken down by denomination, largest first, omitting any
    /// denomination with a zero count.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown if the amount paid is less than the price.
    /// </exception>
    public List<DenominationCount> CalculateChange(decimal amountPaid, decimal price)
    {
        if (amountPaid < price)
        {
            throw new ArgumentException("Amount paid cannot be less than the product price.");
        }

        int remainingPence = ToPence(amountPaid) - ToPence(price);

        var breakdown = new List<DenominationCount>();

        foreach (var (label, pence) in Denominations)
        {
            int count = remainingPence / pence;
            if (count > 0)
            {
                breakdown.Add(new DenominationCount(label, count));
                remainingPence -= count * pence;
            }
        }

        return breakdown;
    }

    private static int ToPence(decimal pounds) => (int)Math.Round(pounds * 100m, MidpointRounding.AwayFromZero);
}
