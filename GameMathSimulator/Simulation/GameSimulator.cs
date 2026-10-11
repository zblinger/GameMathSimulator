public class GameSimulator
{
    private readonly Reel[] _reels;
    private readonly Random _random;

    public GameSimulator(Reel[] reels)
    {
        ArgumentNullException.ThrowIfNull(reels);

        if (reels.Any(reel => reel is null))
            throw new ArgumentException("Reels cannot be null", nameof(reels));

        if (reels.Length != 3)
            throw new ArgumentOutOfRangeException("Exactly 3 reels are required.", nameof(reels));

        _reels = reels.ToArray();
        _random = new();
    }

    public Outcome Spin()
    {
        return new Outcome
        (
            _reels[0].GetSymbol(_random.Next(_reels[0].StopCount)),
            _reels[1].GetSymbol(_random.Next(_reels[1].StopCount)),
            _reels[2].GetSymbol(_random.Next(_reels[2].StopCount))
        );
    }

    public SimulationResult Run(int spinCount, PayTable payTable)
    {
        if (spinCount <= 0) throw new ArgumentOutOfRangeException(nameof(spinCount));
        ArgumentNullException.ThrowIfNull(payTable);

        var result = new SimulationResult();

        double wager = 1.0;
        
        for (int i = 0; i < spinCount; i++)
        {
            var outcome = Spin();
            result.TotalSpins++;
            result.TotalWagered += wager;

            var payout = payTable.GetPayout(outcome);
            result.TotalPaid += payout;
            if (payout > 0)
            {
                result.WinningSpins++;
            }
        }

        return result;
    }
}