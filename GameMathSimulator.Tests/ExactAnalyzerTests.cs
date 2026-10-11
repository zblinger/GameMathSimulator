using Xunit;

public class ExactAnalyzerTests
{
    [Fact]
    public void CalculateTotalProbability_ShouldEqualOne()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();

        // Act
        var result = analyzer.CalculateTotalProbability();

        // Assert
        Assert.Equal(1.0, result, precision: 9);
    }

    [Fact]
    public void CalculateRtp_ShouldReturnExpectedValue()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        // Act
        var result = analyzer.CalculateExpectedPayout(payTable);

        // Assert
        Assert.Equal(1.104, result, precision: 9);
    }

    [Fact]
    public void CalculateHitFrequency_ShouldReturnExpectedValue()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        // Act
        var result = analyzer.CalculateHitFrequency(payTable);

        // Assert
        Assert.Equal(0.064, result, precision: 9);
    }

    [Fact]
    public void GetProbability_ThreeCherries_ShouldReturnExpectedValue()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();

        var outcomeCCC = new Outcome(Symbol.Cherry, Symbol.Cherry, Symbol.Cherry);

        // Act
        var result = analyzer.GetProbability(outcomeCCC);

        // Assert
        Assert.Equal(0.048, result, precision: 9);
    }

    [Fact]
    public void CalculateVariance_ShouldReturnExpectedValue()
    {
        //Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        //Act
        var result = analyzer.CalculateVariance(payTable);

        //Assert
        Assert.Equal(82.413184, result, precision: 9);
    }

    [Fact]
    public void CalculateStandardDeviation_ShouldReturnExpectedValue()
    {
        //Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        //Act
        var result = analyzer.CalculateStandardDeviation(payTable);

        //Assert
        Assert.Equal(9.07817074084862, result, precision: 9);
    }

    [Fact]
    public void CalculateSecondRawMoment_ShouldReturnExpectedValue()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        // Act
        var result = analyzer.CalculateSecondRawMoment(payTable);

        // Assert
        Assert.Equal(83.632, result, precision: 9);
    }

    [Fact]
    public void CalculateStandardErrorForOneHundredThousandSpins_ShouldReturnExpectedValue()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        // Act
        var result = analyzer.CalculateStandardError(payTable, 100000);

        // Assert
        Assert.Equal(0.02870769652898, result, precision: 9);
    }

    [Fact]
    public void CalculateExpectedPayoutIntervalForOneHundredThousandSpins_ShouldReturnExpectedTuple()
    {
        // Arrange
        var analyzer = TestGameFactory.CreateAnalyzer();
        var payTable = new PayTable();

        // Act
        var result = analyzer.CalculateExpectedPayoutInterval(payTable, 100000);

        // Assert
        Assert.Equal(1.0477339487240942, result.Lower, precision: 9);
        Assert.Equal(1.1602660512759058, result.Upper, precision: 9);
    }
}