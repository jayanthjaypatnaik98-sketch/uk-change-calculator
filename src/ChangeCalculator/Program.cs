using ChangeCalculator.Cli;
using ChangeCalculator.Formatting;
using ChangeCalculator.Parsing;
using ChangeCalculator.Services;

// Composition root: the only place that constructs concrete
// implementations. Everything else in the app depends on interfaces.
var app = new ConsoleChangeCalculatorApp(
    new MoneyParser(),
    new ChangeBreakdownService(),
    new ConsoleChangeResultFormatter());

return app.Run(args);
