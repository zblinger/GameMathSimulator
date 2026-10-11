public class Reel
{
    private readonly Symbol[] _symbols;

    public int StopCount => _symbols.Length;

    public Reel(IEnumerable<Symbol> symbols)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        _symbols = symbols.ToArray();

        if (_symbols.Length == 0)
        {
            throw new ArgumentException(
                "A reel must contain at least one symbol.",
                nameof(symbols));
        }
    }

    public Symbol GetSymbol(int position)
    {
        return _symbols[position];
    }

    public double GetProbability(Symbol symbol)
    {
        int count = 0;
        
        for(int i = 0; i < _symbols.Length; i++)
        {
            if(GetSymbol(i) == symbol)
                count++;
        }

        return (double)count/StopCount;
    }
}