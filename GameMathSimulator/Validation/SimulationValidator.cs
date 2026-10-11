public class SimulationValidator
{
    private readonly ExactAnalyzer _analyzer;
    private readonly PayTable _payTable;

    public SimulationValidator(ExactAnalyzer analyzer, PayTable payTable)
    {
        ArgumentNullException.ThrowIfNull(analyzer);
        ArgumentNullException.ThrowIfNull(payTable);

        _analyzer = analyzer;
        _payTable = payTable;
    }

    public bool IsSimulatedRtpWithinExpectedRange(SimulationResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var interval = _analyzer.CalculateExpectedPayoutInterval(_payTable, result.TotalSpins);

        return result.Rtp >= interval.Lower && result.Rtp <= interval.Upper;
    }
}