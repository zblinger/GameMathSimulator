using Xunit;

public class SimulationValidatorTests
{
    [Fact]
    public void IsSimulatedRtpWithinExpectedRange_ShouldReturnTrue_WhenRtpIsInsideInterval()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();
        var validator = new SimulationValidator(analyzer, payTable);
        var result = new SimulationResult
        {
            TotalSpins = 100_000,
            TotalWagered = 100_000,
            TotalPaid = 110_400
        };

        // Act
        var isWithinExpectedRange = validator.IsSimulatedRtpWithinExpectedRange(result);
        
        // Assert
        Assert.True(isWithinExpectedRange);
    }

    [Fact]
    public void IsSimulatedRtpWithinExpectedRange_ShouldReturnFalse_WhenRtpIsOutsideInterval()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();
        var validator = new SimulationValidator(analyzer, payTable);
        var result = new SimulationResult
        {
            TotalSpins = 100_000,
            TotalWagered = 100_000,
            TotalPaid = 130_000
        };

        // Act
        var isWithinExpectedRange = validator.IsSimulatedRtpWithinExpectedRange(result);

        // Assert
        Assert.False(isWithinExpectedRange);
    }
}
