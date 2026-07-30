namespace ChangeCalculator.Domain;

/// <summary>
/// A single UK coin or note denomination. Values are stored in pence
/// (whole numbers) rather than pounds as decimals/floats, so that
/// change calculations never suffer floating point rounding errors.
/// </summary>
public sealed record Denomination(int ValueInPence, string DisplayName)
{
    /// <summary>
    /// All UK coins and notes currently in circulation, ordered
    /// largest first as required by the change-breakdown algorithm.
    /// </summary>
    public static readonly IReadOnlyList<Denomination> UkDenominations = new List<Denomination>
    {
        new(5000, "£50"),
        new(2000, "£20"),
        new(1000, "£10"),
        new(500,  "£5"),
        new(200,  "£2"),
        new(100,  "£1"),
        new(50,   "50p"),
        new(20,   "20p"),
        new(10,   "10p"),
        new(5,    "5p"),
        new(2,    "2p"),
        new(1,    "1p"),
    }.AsReadOnly();
}
