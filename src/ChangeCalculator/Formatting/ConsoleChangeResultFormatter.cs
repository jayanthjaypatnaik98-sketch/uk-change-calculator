using System.Text;
using ChangeCalculator.Domain;

namespace ChangeCalculator.Formatting;

public sealed class ConsoleChangeResultFormatter : IChangeResultFormatter
{
    public string Format(ChangeCalculationResult result)
    {
        if (!result.IsSuccess)
        {
            return $"Error: {result.ErrorMessage}";
        }

        if (result.Breakdown.Count == 0)
        {
            return "Your change is: no change due.";
        }

        var builder = new StringBuilder();
        builder.AppendLine("Your change is:");

        foreach (var line in result.Breakdown)
        {
            builder.AppendLine($"{line.Count} x {line.Denomination.DisplayName}");
        }

        return builder.ToString().TrimEnd();
    }
}
