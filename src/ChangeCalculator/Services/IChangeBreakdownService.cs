using ChangeCalculator.Domain;

namespace ChangeCalculator.Services;

/// <summary>
/// Pure business logic: given an amount paid and a price (both in pence),
/// work out the change breakdown. No console I/O, no parsing - just maths.
/// </summary>
public interface IChangeBreakdownService
{
    ChangeCalculationResult Calculate(long amountPaidInPence, long priceInPence);
}
