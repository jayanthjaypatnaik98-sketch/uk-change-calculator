namespace ChangeCalculator;

public record DenominationCount(string Label, int Count);

public class ChangeBreakdownService
{
    
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
