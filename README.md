# UK Change Calculator

Given a UK currency amount paid and the price of a product, this app calculates
the change due and displays it broken down by denomination, largest first.

## Features
 
- Calculates change using UK denominations.
- Returns the minimum number of notes and coins.
- Handles invalid input.
- Detects insufficient payment.
- Unit tested using xUnit.
- Built using .NET 8.
 
## Project Structure
 
```
src/
└── ChangeCalculator
    ├── Cli
    ├── Domain
    ├── Formatting
    ├── Parsing
    ├── Services
    ├── Program.cs
    └── ChangeCalculator.csproj
 
tests/
└── ChangeCalculator.Tests
    ├── Formatting
    ├── Parsing
    ├── Services
    └── ChangeCalculator.Tests.csproj
```
 
## Architecture
 
### Cli
 
Responsible for interacting with the user, reading input, and displaying output.
 
### Parsing
 
Validates and converts user input into monetary values.
 
### Services
 
Contains the business logic used to calculate the optimal UK change breakdown.
 
### Formatting
 
Formats the calculation result into a user-friendly console output.
 
### Domain
 
Contains the application's domain models and shared data structures.
 
## Example
 
Input
 
```
Amount Paid : 20
Product Price : 5.50
```
 
Output
 
```
Change Due : £14.50
 
£10 x 1
£2 x 2
50p x 1
```
 
## Error Handling
 
Examples:
 
```
Invalid amount paid. Please enter a valid numeric value (e.g. 20 or 20.50).
```
 
```
Invalid product price. Please enter a valid numeric value (e.g. 10 or 10.99).
```
 
```
Insufficient payment.
```
 
## Technologies
 
- C#
- .NET 8
- xUnit
 
## Design Principles
 
- Separation of Concerns
- SOLID Principles
- Dependency Injection
- Interface-based design
- Testable architecture
 
## Running the Application
 
Open the solution in Visual Studio.
 
Build the solution.
 
Run the console application.
 
Enter:
 
- Amount Paid
- Product Price
 
The application displays the optimal UK change breakdown.
 
## Running Tests
 
Visual Studio
 
```
Test -> Run All Tests
```
 
or
 
```
dotnet test
```
