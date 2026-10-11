public static class TestGameFactory
{
    public static Reel[] CreateReels()
    {
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

        return [reel1, reel2, reel3];
    }

    public static ExactAnalyzer CreateAnalyzer()
    {
        var reels = CreateReels();

        return new ExactAnalyzer(reels);
    }
}