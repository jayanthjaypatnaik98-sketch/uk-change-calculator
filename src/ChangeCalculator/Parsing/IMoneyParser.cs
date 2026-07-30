namespace ChangeCalculator.Parsing;

/// <summary>
/// Converts user-supplied currency text (e.g. "20", "5.50", "£5.50")
/// into whole pence, so downstream code never touches raw strings or floats.
/// </summary>
public interface IMoneyParser
{
    bool TryParse(string input, out long amountInPence);
}
