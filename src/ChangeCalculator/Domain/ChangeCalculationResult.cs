namespace ChangeCalculator.Domain;

/// <summary>
/// The outcome of a change calculation. Using a result object instead of
/// exceptions/return codes keeps "insufficient payment" as an expected,
/// explicit branch rather than exceptional control flow.
/// </summary>
public sealed class ChangeCalculationResult
{
    public bool IsSuccess { get; }
    public IReadOnlyList<ChangeBreakdownLine> Breakdown { get; }
    public string? ErrorMessage { get; }

    private ChangeCalculationResult(bool isSuccess, IReadOnlyList<ChangeBreakdownLine> breakdown, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Breakdown = breakdown;
        ErrorMessage = errorMessage;
    }

    public static ChangeCalculationResult Success(IReadOnlyList<ChangeBreakdownLine> breakdown) =>
        new(isSuccess: true, breakdown, errorMessage: null);

    public static ChangeCalculationResult Failure(string errorMessage) =>
        new(isSuccess: false, breakdown: Array.Empty<ChangeBreakdownLine>(), errorMessage);
}
