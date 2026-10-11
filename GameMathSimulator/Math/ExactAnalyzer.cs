public class ExactAnalyzer
{
    private readonly Reel[] _reels;
    public int ReelsCount => _reels.Length;
    public IReadOnlyList<int> ReelStopCounts => _reels.Select(reel => reel.StopCount).ToArray();
    public long TotalStopCombinations => _reels.Aggregate(1L, (total, reel) => total * reel.StopCount);

    public ExactAnalyzer(Reel[] reels)
    {
        ArgumentNullException.ThrowIfNull(reels);

        if (reels.Any(reel => reel is null))
            throw new ArgumentException("Reels cannot be null", nameof(reels));

        if (reels.Length != 3)
            throw new ArgumentOutOfRangeException("Exactly 3 reels are required.", nameof(reels));

        _reels = reels.ToArray();
    }

    public double GetProbability(Outcome outcome)
    {
        return _reels[0].GetProbability(outcome.Reel1) * _reels[1].GetProbability(outcome.Reel2) * _reels[2].GetProbability(outcome.Reel3);
    }

    public IEnumerable<Outcome> GetAllOutcomes()
    {
        foreach (Symbol a in Enum.GetValues<Symbol>())
        {
            foreach (Symbol b in Enum.GetValues<Symbol>())
            {
                foreach (Symbol c in Enum.GetValues<Symbol>())
                {
                    yield return new Outcome(a, b, c);
                }
            }
        }
    }

    public double CalculateExpectedPayout(PayTable payTable)
    {
        double ep = 0;
        foreach (var outcome in GetAllOutcomes())
        {
            var probability = GetProbability(outcome);
            var payout = payTable.GetPayout(outcome);
            ep += (probability * payout);
        }

        return ep;
    }

    public double CalculateSecondRawMoment(PayTable payTable)
    {
        double srm = 0;
        foreach (var outcome in GetAllOutcomes())
        {
            var probability = GetProbability(outcome);
            var payout = payTable.GetPayout(outcome);
            srm += probability * payout * payout;
        }

        return srm;
    }

    public double CalculateHitFrequency(PayTable payTable)
    {
        double hf = 0;
        foreach (var outcome in GetAllOutcomes())
        {
            if (payTable.GetPayout(outcome) > 0)
                hf += GetProbability(outcome);
        }

        return hf;
    }

    public double CalculateTotalProbability()
    {
        double tp = 0;
        foreach (var outcome in GetAllOutcomes())
        {
            tp += GetProbability(outcome);
        }
        
        return tp;
    }

    public double CalculateVariance(PayTable payTable)
    {
        double ep = CalculateExpectedPayout(payTable);
        return CalculateSecondRawMoment(payTable) - (ep * ep);
    }

    public double CalculateStandardDeviation(PayTable payTable)
    {
        return Math.Sqrt(CalculateVariance(payTable));
    }

    public double CalculateStandardError(PayTable payTable, int spinCount)
    {
        if (spinCount <= 0) throw new ArgumentOutOfRangeException(nameof(spinCount));
        double sd = CalculateStandardDeviation(payTable);
        return sd / Math.Sqrt(spinCount);
    }

    public (double Lower, double Upper) CalculateExpectedPayoutInterval(PayTable payTable, int spinCount)
    {
        const double ZCriticalValue95Percent = 1.959963984540054;

        double expectedPayout = CalculateExpectedPayout(payTable);
        double standardError = CalculateStandardError(payTable, spinCount);

        double marginOfError = ZCriticalValue95Percent * standardError;

        return (
            expectedPayout - marginOfError,
            expectedPayout + marginOfError
        );
    }
}