using ChangeCalculator.Formatting;
using ChangeCalculator.Parsing;
using ChangeCalculator.Services;

namespace ChangeCalculator.Cli;

/// <summary>
/// Orchestrates parsing, calculation and formatting, and owns all console
/// I/O. Deliberately contains no business rules of its own - if you're
/// tempted to add an "if" about money here, it belongs in a service instead.
/// </summary>
public sealed class ConsoleChangeCalculatorApp
{
    private const string UsageMessage =
        "Usage: dotnet run --project src/ChangeCalculator -- <amount-paid> <price>\n" +
        "Example: dotnet run --project src/ChangeCalculator -- 20 5.50";

    private readonly IMoneyParser _moneyParser;
    private readonly IChangeBreakdownService _changeBreakdownService;
    private readonly IChangeResultFormatter _formatter;

    public ConsoleChangeCalculatorApp(
        IMoneyParser moneyParser,
        IChangeBreakdownService changeBreakdownService,
        IChangeResultFormatter formatter)
    {
        _moneyParser = moneyParser;
        _changeBreakdownService = changeBreakdownService;
        _formatter = formatter;
    }

    /// <returns>Process exit code: 0 on success, 1 on any failure.</returns>
    /// <returns>Process exit code: 0 on success, 1 on any failure.</returns>
    public int Run(string[] args)
    {
        Console.WriteLine("=== UK Change Calculator ===");
        Console.WriteLine();

        Console.Write("Enter Amount Paid: ");
        string? amountPaid = Console.ReadLine();

        Console.Write("Enter Product Price: ");
        string? price = Console.ReadLine();

        if (!_moneyParser.TryParse(amountPaid ?? string.Empty, out var amountPaidInPence))
        {
            Console.WriteLine("Invalid amount paid. Please enter a valid numeric value (e.g., 20 or 20.50).");
            return 1;
        }

        if (!_moneyParser.TryParse(price ?? string.Empty, out var priceInPence))
        {
            Console.WriteLine("Invalid product price. Please enter a valid numeric value (e.g., 10 or 10.99).");
            return 1;
        }

        var result = _changeBreakdownService.Calculate(amountPaidInPence, priceInPence);

        Console.WriteLine();
        Console.WriteLine(_formatter.Format(result));

        return result.IsSuccess ? 0 : 1;
    }
}
