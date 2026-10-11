public class PayTable
{
    private readonly Dictionary<Outcome, double> _payTable = new();

    public PayTable(/* some way to provide payouts */)
    {
        _payTable.Add(new Outcome(Symbol.Cherry, Symbol.Cherry, Symbol.Cherry), 3.0);
        _payTable.Add(new Outcome(Symbol.Lemon, Symbol.Lemon, Symbol.Lemon), 5.0);
        _payTable.Add(new Outcome(Symbol.Bell, Symbol.Bell, Symbol.Bell), 20.0);
        _payTable.Add(new Outcome(Symbol.Seven, Symbol.Seven, Symbol.Seven), 100.0);
    }

    public double GetPayout(Outcome outcome)
    {
        return _payTable.TryGetValue(outcome, out double value)
            ? value
            : 0.0;
    }
}