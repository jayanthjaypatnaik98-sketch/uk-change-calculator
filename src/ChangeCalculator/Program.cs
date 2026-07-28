using ChangeCalculator;
using System.Globalization;

var service = new ChangeBreakdownService();

// Supports two ways of running the app so it's easy for anyone (including an
// automated build/test process) to use it:
//   1. dotnet run --project src/ChangeCalculator -- 20 5.50
//   2. dotnet run --project src/ChangeCalculator      (then answer the prompts)
decimal amountPaid;
decimal price;

if (args.Length >= 2)
{
    if (!TryParseMoney(args[0], out amountPaid) || !TryParseMoney(args[1], out price))
    {
        Console.WriteLine("Could not understand the amounts provided. Example usage:");
        Console.WriteLine("  dotnet run --project src/ChangeCalculator -- 20 5.50");
        return 1;
    }
}
else
{
    Console.WriteLine("=== UK Change Calculator ===");
    Console.WriteLine();

    amountPaid = ReadMoney("Enter the amount paid (e.g. 20 or £20): ");
    price = ReadMoney("Enter the product price (e.g. 5.50 or £5.50): ");
}

try
{
    var breakdown = service.CalculateChange(amountPaid, price);

    Console.WriteLine();
    if (breakdown.Count == 0)
    {
        Console.WriteLine("No change due.");
    }
    else
    {
        Console.WriteLine("Your change is:");
        foreach (var item in breakdown)
        {
            Console.WriteLine($"{item.Count} x {item.Label}");
        }
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine();
    Console.WriteLine($"Error: {ex.Message}");
    return 1;
}

return 0;

// Reads a line of console input in a loop until it parses as a valid money value.
static decimal ReadMoney(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (TryParseMoney(input, out var value))
        {
            return value;
        }

        Console.WriteLine("That doesn't look like a valid amount. Please try again (e.g. 5.50 or £5.50).");
    }
}

// Accepts values with or without a leading "£" and with or without decimal places.
static bool TryParseMoney(string? input, out decimal value)
{
    value = 0m;
    if (string.IsNullOrWhiteSpace(input))
    {
        return false;
    }

    var cleaned = input.Trim().TrimStart('£').Trim();

    return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
           && value >= 0m;
}
