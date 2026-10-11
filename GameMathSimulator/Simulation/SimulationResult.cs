public class SimulationResult
{
    public int TotalSpins { get; set; }
    public int WinningSpins { get; set; }
    public double TotalWagered { get; set; }
    public double TotalPaid { get; set; }

    public double Rtp =>
        TotalWagered == 0 ? 0 : TotalPaid / TotalWagered;

    public double HitFrequency =>
        TotalSpins == 0 ? 0 : (double)WinningSpins / TotalSpins;
}