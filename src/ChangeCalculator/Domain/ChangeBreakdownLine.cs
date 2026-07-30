namespace ChangeCalculator.Domain;

/// <summary>
/// How many of a given denomination should be returned as change.
/// </summary>
public sealed record ChangeBreakdownLine(Denomination Denomination, int Count);
