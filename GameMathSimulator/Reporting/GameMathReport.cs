public class GameMathReport
{
    private readonly ExactAnalyzer _analyzer;
    private readonly PayTable _payTable;
    private readonly SimulationResult _simulationResult;
    private readonly SimulationValidator _simulationValidator;

    public GameMathReport(
        ExactAnalyzer analyzer,
        PayTable payTable,
        SimulationResult simulationResult,
        SimulationValidator simulationValidator)
    {
        ArgumentNullException.ThrowIfNull(analyzer);
        ArgumentNullException.ThrowIfNull(payTable);
        ArgumentNullException.ThrowIfNull(simulationResult);
        ArgumentNullException.ThrowIfNull(simulationValidator);

        _analyzer = analyzer;
        _payTable = payTable;
        _simulationResult = simulationResult;
        _simulationValidator = simulationValidator;
    }

    public void Print()
    {
        var interval = _analyzer.CalculateExpectedPayoutInterval(_payTable, _simulationResult.TotalSpins);

        Console.WriteLine("=========================================");
        Console.WriteLine("         GAME MATH SIMULATOR");
        Console.WriteLine("=========================================\n");
        Console.WriteLine("GAME CONFIGURATION");
        Console.WriteLine($"Reels:                      {_analyzer.ReelsCount}");
        foreach (var (stopCount, index) in _analyzer.ReelStopCounts.Select((count, index) => (count, index)))
        {
            Console.WriteLine($"Reel {index + 1} Stops:               {stopCount}");
        }
        Console.WriteLine($"Total Stop Combinations:    {_analyzer.TotalStopCombinations:N0}");
        Console.WriteLine("\nTHEORETICAL ANALYSIS");
        Console.WriteLine($"Expected Payout:            {_analyzer.CalculateExpectedPayout(_payTable):F6}");
        Console.WriteLine($"Theoretical RTP:            {_analyzer.CalculateExpectedPayout(_payTable):P3}");
        Console.WriteLine($"Hit Frequency:              {_analyzer.CalculateHitFrequency(_payTable):P3}");
        Console.WriteLine($"Variance:                   {_analyzer.CalculateVariance(_payTable):F6}");
        Console.WriteLine($"Standard Deviation:         {_analyzer.CalculateStandardDeviation(_payTable):F6}");
        Console.WriteLine("\nMONTE CARLO SIMULATION");
        Console.WriteLine($"Spins:                      {_simulationResult.TotalSpins:N0}");
        Console.WriteLine($"Total Wagered:              {_simulationResult.TotalWagered:N0}");
        Console.WriteLine($"Total Paid:                 {_simulationResult.TotalPaid:N0}");
        Console.WriteLine($"Simulated RTP:              {_simulationResult.Rtp:P3}");
        Console.WriteLine($"Simulated Hit Rate:         {_simulationResult.HitFrequency:P3}");
        Console.WriteLine("\nSTATISTICAL VALIDATION");
        Console.WriteLine($"Standard Error:             {_analyzer.CalculateStandardError(_payTable, _simulationResult.TotalSpins):F6}");
        Console.WriteLine($"95% RTP Range:              {interval.Lower:P3} - {interval.Upper:P3}");
        Console.WriteLine($"Observed RTP:               {_simulationResult.Rtp:P3}");
        Console.WriteLine($"RTP Within Expected Range:  {(_simulationValidator.IsSimulatedRtpWithinExpectedRange(_simulationResult) ? "YES" : "NO")}");
        Console.WriteLine("\n=========================================");
    }
}
