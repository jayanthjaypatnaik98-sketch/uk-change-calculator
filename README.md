# UK Change Calculator

Given a UK currency amount paid and the price of a product, this app calculates
the change due and displays it broken down by denomination, largest first.

## Example

```
Input:  Paid £20, Price £5.50
Output:
Your change is:
1 x £10
2 x £2
1 x 50p
```

## How it works

- `ChangeBreakdownService` (in `src/ChangeCalculator/ChangeBreakdownService.cs`)
  contains the core logic. It converts pounds to whole pence up front (to avoid
  floating point rounding errors with money), then uses a greedy algorithm:
  for each denomination from largest (£50) to smallest (1p), take as many of
  that coin/note as fit into the remaining change, then move to the next one.
- `Program.cs` handles reading input and printing the result. It accepts input
  either as command-line arguments or via interactive prompts, and understands
  amounts with or without a "£" sign (e.g. `20`, `£20`, `5.50`, `£5.50`).
- `ChangeCalculator.Tests` contains xUnit tests, including the exact example
  from the task description, plus edge cases (exact payment, tiny coin amounts,
  paying less than the price, large amounts).

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (free download,
  works on Windows/Mac/Linux)

## How to build and run

Open a terminal in the repository's root folder (the one containing
`ChangeCalculator.sln`) and run:

```bash
# Restore and build everything
dotnet build

# Run interactively (it will ask you for the amount paid and the price)
dotnet run --project src/ChangeCalculator

# OR run non-interactively by passing the values as arguments
dotnet run --project src/ChangeCalculator -- 20 5.50
```

## How to run the tests

```bash
dotnet test
```

This runs all 5 unit tests in `ChangeCalculator.Tests`, including a test that
verifies the exact example given in the task (£20 paid, £5.50 price → 1 x £10,
2 x £2, 1 x 50p).

## Project structure

```
ChangeCalculator.sln
src/
  ChangeCalculator/
    ChangeCalculator.csproj
    Program.cs
    ChangeBreakdownService.cs
tests/
  ChangeCalculator.Tests/
    ChangeCalculator.Tests.csproj
    ChangeBreakdownServiceTests.cs
```
