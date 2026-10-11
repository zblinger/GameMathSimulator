var reel1 = new Reel(new[]
{
    Symbol.Cherry,
    Symbol.Cherry,
    Symbol.Lemon,
    Symbol.Bell,
    Symbol.Seven
});

var reel2 = new Reel(new[]
{
    Symbol.Cherry,
    Symbol.Lemon,
    Symbol.Lemon,
    Symbol.Bell,
    Symbol.Seven
});

var reel3 = new Reel(new[]
{
    Symbol.Cherry,
    Symbol.Cherry,
    Symbol.Cherry,
    Symbol.Bell,
    Symbol.Seven
});

Reel[] reels = {reel1, reel2, reel3};
var analyzer = new ExactAnalyzer(reels);
var simulator = new GameSimulator(reels);
var payTable = new PayTable();
int spinCount = 100000;
var simulationResult = simulator.Run(spinCount, payTable);
var validator = new SimulationValidator(analyzer, payTable);

var report = new GameMathReport(analyzer, payTable, simulationResult, validator);
report.Print();