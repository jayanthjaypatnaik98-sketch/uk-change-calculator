using ChangeCalculator.Domain;

namespace ChangeCalculator.Formatting;

/// <summary>
/// Turns a calculation result into user-facing text. Kept separate from the
/// service so the output format (console today, JSON/API tomorrow) can change
/// without touching business logic.
/// </summary>
public interface IChangeResultFormatter
{
    string Format(ChangeCalculationResult result);
}
