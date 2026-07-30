using ChangeCalculator.Domain;

namespace ChangeCalculator.Services;

public sealed class ChangeBreakdownService : IChangeBreakdownService
{
    public ChangeCalculationResult Calculate(long amountPaidInPence, long priceInPence)
    {
        if (amountPaidInPence < 0 || priceInPence < 0)
        {
            return ChangeCalculationResult.Failure("Amounts cannot be negative.");
        }

        if (amountPaidInPence < priceInPence)
        {
            return ChangeCalculationResult.Failure("Amount paid is less than the price.");
        }

        var remainingPence = amountPaidInPence - priceInPence;
        var breakdown = new List<ChangeBreakdownLine>();

        foreach (var denomination in Denomination.UkDenominations)
        {
            var count = (int)(remainingPence / denomination.ValueInPence);
            if (count <= 0)
            {
                continue;
            }

            breakdown.Add(new ChangeBreakdownLine(denomination, count));
            remainingPence -= count * denomination.ValueInPence;
        }

        return ChangeCalculationResult.Success(breakdown);
    }
}
