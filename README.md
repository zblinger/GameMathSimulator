# Game Math Simulator

A C# / .NET 10 application that performs exact mathematical analysis and Monte Carlo simulation of a three-reel slot game.

The project demonstrates probability modeling, combinatorics, statistical analysis, and simulation techniques used in game mathematics.

## Overview

Game Math Simulator calculates the theoretical performance of a simplified slot game and compares those calculations against the results of randomly simulated spins.

The application uses exact symbol probabilities to calculate expected payout, return to player (RTP), hit frequency, variance, and standard deviation. It then performs a Monte Carlo simulation and evaluates whether the observed RTP falls within an approximate 95% expected sampling range.

The project was developed to apply mathematical and statistical concepts through C# programming, with an emphasis on understanding the relationship between theoretical probability distributions and simulated outcomes.

### Project Scope

This is an educational demonstration rather than a commercially balanced slot game.

The current implementation models three independent reels with five stops each, a single evaluated payline, and a fixed pay table.

The theoretical RTP is **110.4%**, meaning the expected payout exceeds the amount wagered. This configuration is intentionally retained to demonstrate the mathematical calculations and statistical validation process rather than to represent a commercially viable game.

The initial version focuses on mathematical correctness, simulation, automated testing, and console reporting.

## Features

### Game Simulation

- Three-reel slot game with configurable reel strips.
- Independent, uniformly random reel-stop selection.
- Symbol-based outcomes and pay-table evaluation.
- Monte Carlo simulation with configurable spin count.
- Tracking of total spins, total wagered, total paid, and winning spins.

### Mathematical Analysis

- Exact symbol probability calculations.
- Expected payout and theoretical RTP.
- Theoretical hit frequency.
- Second raw moment of the payout distribution.
- Variance and standard deviation.
- Standard error of the simulated mean payout.
- Approximate 95% sampling range for simulated RTP.

### Statistical Validation

- Comparison of simulated RTP against theoretical expectations.
- PASS/FAIL reporting based on whether the observed RTP falls within the expected sampling range.
- Automated unit tests using xUnit.
- Shared test-game configuration through a test factory.

## Game Configuration

The example game contains three reels with five stops each.

Each spin independently selects one stop from each reel with equal probability.

| Stop | Reel 1 | Reel 2 | Reel 3 |
|---|---|---|---|
| 1 | Cherry | Cherry | Cherry |
| 2 | Cherry | Lemon | Cherry |
| 3 | Lemon | Lemon | Cherry |
| 4 | Bell | Bell | Bell |
| 5 | Seven | Seven | Seven |

### Total Stop Combinations

The total number of possible reel-stop combinations is the product of the stop counts:

$$
5 \times 5 \times 5 = 125
$$

All 125 stop combinations are equally likely.

However, multiple stop combinations can produce the same visible symbol outcome because some symbols appear more than once on a reel.

For example, Cherry appears twice on Reel 1, once on Reel 2, and three times on Reel 3.

Therefore, the probability of three Cherries is:

$$
P(CCC) = \frac{2}{5} \times \frac{1}{5} \times \frac{3}{5}
$$

$$
P(CCC) = \frac{6}{125} = 0.048
$$

This corresponds to a 4.8% probability.

### Pay Table

The game uses a fixed pay table containing the following winning combinations:

| Combination | Payout | Probability |
|---|---:|---:|
| Cherry–Cherry–Cherry | 3 | 4.8% |
| Lemon–Lemon–Lemon | 5 | 0.0% |
| Bell–Bell–Bell | 20 | 0.8% |
| Seven–Seven–Seven | 100 | 0.8% |

All other combinations return zero.

Payouts represent total credits returned for a one-credit wager.

The Lemon combination is included in the pay table but cannot occur because Reel 3 does not contain a Lemon symbol. Consequently, its probability and contribution to expected payout are zero.

## Mathematical Model

The mathematical analyzer evaluates the probability distribution of possible symbol outcomes.

Rather than enumerating every physical reel-stop combination, the analyzer enumerates distinct symbol combinations and calculates their probabilities using the symbol frequencies on each reel.

For a three-reel outcome:

$$
P(A,B,C) = P_1(A) \times P_2(B) \times P_3(C)
$$

where each probability represents the frequency of the corresponding symbol on its reel.

This approach accounts for repeated symbols while avoiding double-counting equivalent visible outcomes.

### Expected Payout

Expected payout represents the probability-weighted average payout over all possible outcomes.

$$
E[X] = \sum_i P(x_i)x_i
$$

where:

- $X$ is the payout from a single spin.
- $P(x_i)$ is the probability of outcome $i$.
- $x_i$ is the payout associated with that outcome.

For the current game:

$$
E[X] = 3(0.048) + 5(0) + 20(0.008) + 100(0.008)
$$

$$
E[X] = 0.144 + 0 + 0.160 + 0.800
$$

$$
\boxed{E[X] = 1.104}
$$

The theoretical expected payout is 1.104 credits per spin.

### Return to Player (RTP)

Return to player represents the expected proportion of wagered credits returned to the player.

$$
RTP = \frac{E[X]}{\text{Wager Per Spin}} \times 100\text{\%}
$$

With a wager of one credit:

$$
RTP = \frac{1.104}{1} \times 100\text{\%}
$$

$$
\boxed{\mathrm{RTP} = 110.4\text{\%}}
$$

This is the theoretical RTP derived from the game's probability distribution.

Actual RTP observed during a finite simulation will vary because outcomes are randomly generated.

### Hit Frequency

Hit frequency represents the probability that a spin produces a positive payout.

$$
HF = P(X > 0)
$$

For the current pay table:

$$
HF = 0.048 + 0.008 + 0.008
$$

$$
\boxed{\mathrm{HF} = 0.064 = 6.4\text{\%}}
$$

Therefore, approximately 6.4% of spins are expected to produce a winning outcome.

This is a theoretical probability, not a guarantee that exactly 6.4% of spins will win during a particular simulation.

### Second Raw Moment

The second raw moment is used to calculate payout variance.

$$
E[X^2] = \sum_i P(x_i)x_i^2
$$

For the current game:

$$
E[X^2] = 3^2(0.048) + 20^2(0.008) + 100^2(0.008)
$$

$$
\boxed{E[X^2] = 83.632}
$$

### Variance

Variance measures the spread of payouts around their expected value.

$$
Var(X) = E[X^2] - E[X]^2
$$

Substituting the calculated values:

$$
Var(X) = 83.632 - 1.104^2
$$

$$
\boxed{Var(X) = 82.413184}
$$

A larger variance indicates greater variation in individual payouts.

### Standard Deviation

Standard deviation is the square root of variance.

$$
\sigma = \sqrt{Var(X)}
$$

$$
\sigma = \sqrt{82.413184}
$$

$$
\boxed{\sigma \approx 9.078171}
$$

Standard deviation expresses payout variability in the same units as the payout itself.

The relatively large standard deviation reflects the presence of an infrequent 100-credit payout.

### Standard Error

Standard error describes the variability of the simulated mean payout across repeated simulations.

For $n$ independent spins:

$$
SE = \frac{\sigma}{\sqrt{n}}
$$

For 100,000 spins:

$$
SE = \frac{9.078171}{\sqrt{100000}}
$$

$$
\boxed{SE \approx 0.028708}
$$

As the number of simulated spins increases, standard error decreases.

This means that larger simulations generally produce RTP estimates closer to the theoretical value.

### Approximate 95% RTP Sampling Range

The application calculates an approximate 95% sampling range for the simulated mean payout using the theoretical expected payout and standard error.

$$
\text{Lower Bound} = E[X] - 1.96(SE)
$$

$$
\text{Upper Bound} = E[X] + 1.96(SE)
$$

For 100,000 spins:

$$
1.104 \pm 1.96(0.028708)
$$

The resulting approximate RTP range is:

$$
\boxed{104.773\text{\%} \leq \mathrm{RTP} \leq 116.027\text{\%}}
$$

The application compares the simulated RTP against these bounds.

A simulation receives a PASS result when its observed RTP falls within the calculated range.

**Important:** This is an approximate sampling range centered on the known theoretical RTP, not a confidence interval estimated from the observed simulation data.

The calculation uses a normal approximation to the distribution of the sample mean.

A correctly implemented simulation may occasionally fall outside the expected range due to random variation. Likewise, falling inside the range does not prove that the implementation is correct.

## Monte Carlo Simulation

The simulator models individual spins by randomly selecting one stop from each reel.

For each spin:

1. Generate a random stop position for each reel.
2. Retrieve the symbol at each selected stop.
3. Construct an outcome from the resulting symbols.
4. Evaluate the outcome using the pay table.
5. Update the simulation totals.

After completing the requested number of spins, the application calculates:

- Total spins.
- Total credits wagered.
- Total credits paid.
- Number of winning spins.
- Simulated RTP.
- Simulated hit frequency.

Simulated RTP is calculated as:

$$
\text{Simulated RTP} =
\frac{\text{Total Paid}}{\text{Total Wagered}}
$$

Simulated hit frequency is calculated as:

$$
\text{Simulated Hit Frequency} =
\frac{\text{Winning Spins}}{\text{Total Spins}}
$$

These statistics are then compared against the exact theoretical calculations.

## Example Console Output

The following output was produced by a simulation of 100,000 spins.

Because the simulator uses random outcomes, results will vary between executions.

```text
=========================================
         GAME MATH SIMULATOR
=========================================

GAME CONFIGURATION
Reels:                      3
Stops per Reel:             5
Total Stop Combinations:    125

THEORETICAL ANALYSIS
Expected Payout:            1.104
Theoretical RTP:            110.400%
Hit Frequency:              6.400%
Variance:                   82.413184
Standard Deviation:         9.078171

MONTE CARLO SIMULATION
Spins:                      100,000
Total Wagered:              100,000
Total Paid:                 114,296
Simulated RTP:              114.296%
Simulated Hit Rate:         6.402%

STATISTICAL VALIDATION
Standard Error:             0.028708
95% RTP Range:              104.773% - 116.027%
Observed RTP:               114.296%
RTP Validation:             PASS

=========================================
```

In this example, the simulated RTP was 114.296%, compared with a theoretical RTP of 110.400%.

The observed result falls within the approximate 95% sampling range, so the statistical validator reports PASS.

The difference between theoretical and simulated RTP illustrates the variability expected from a finite number of random trials.

## Getting Started

### Requirements

- .NET 10 SDK
- A terminal capable of running .NET CLI commands
- An IDE or code editor supporting C# (optional)

### Clone the Repository

```bash
git clone https://github.com/zblinger/GameMathSimulator.git
cd GameMathSimulator
```

### Build the Application

From the repository root:

```bash
dotnet build
```

### Run the Simulator

Run the console application:

```bash
dotnet run --project GameMathSimulator
```

The application performs the configured number of spins and prints the mathematical analysis, simulation results, and statistical validation report.

The simulation spin count can be changed in `Program.cs`.

### Run the Tests

Execute the automated test suite:

```bash
dotnet test
```

The test project uses xUnit to verify mathematical calculations and statistical validation behavior.

Latest verified test results:

```text
Total Tests: 11
Passed:      11
Failed:      0
Skipped:     0
```

## Testing and Validation

The project includes automated unit tests for the mathematical analyzer and statistical validator.

Tests cover calculations and behavior including:

- Total probability normalization.
- Probability of a specific symbol outcome.
- Expected payout.
- Hit frequency.
- Second raw moment.
- Variance.
- Standard deviation.
- Standard error.
- Approximate 95% sampling interval.
- Acceptance of simulated RTP inside the expected range.
- Rejection of simulated RTP outside the expected range.

The tests use a shared `TestGameFactory` to construct a consistent three-reel configuration.

Expected mathematical values are calculated independently and compared against the application's results.

Floating-point calculations are tested using appropriate numerical precision.

The statistical validator compares simulated RTP against an interval derived from the theoretical payout distribution and simulation size.

This provides a consistency check between exact mathematical analysis and Monte Carlo simulation.

However, statistical validation alone cannot establish that an implementation is correct. Automated tests, mathematical reasoning, and independent verification remain important.

## Architecture

The application separates the mathematical model, simulation, validation, and reporting responsibilities.

### Reel

Represents an individual reel containing an ordered sequence of symbols.

Provides access to reel stops and calculates the probability of individual symbols.

### Outcome

Represents the three symbols produced by a spin.

Uses value-based equality to support outcome comparisons and pay-table lookups.

### PayTable

Maps winning symbol combinations to their corresponding payouts.

Outcomes without a defined payout return zero.

### ExactAnalyzer

Performs theoretical mathematical calculations using the configured reel strips and pay table.

Calculates outcome probabilities, expected payout, hit frequency, payout moments, variance, standard deviation, standard error, and expected sampling ranges.

### GameSimulator

Performs Monte Carlo simulation by generating independent random reel-stop positions.

Evaluates outcomes and accumulates simulation statistics.

### SimulationResult

Stores simulation totals and exposes calculated statistics such as simulated RTP and hit frequency.

### SimulationValidator

Compares simulated RTP against the theoretical approximate 95% sampling range.

### GameMathReport

Formats and displays the game configuration, mathematical analysis, simulation results, and statistical validation.

### TestGameFactory

Provides a consistent reel configuration for automated testing.

## Limitations

The current version intentionally focuses on a simplified mathematical model.

Known limitations include:

- The game uses exactly three reels.
- Each spin evaluates one symbol from each reel.
- The pay table uses exact three-symbol combinations.
- The game does not include wild symbols, scatter symbols, bonus features, or multiple paylines.
- The wager is fixed at one credit per spin.
- The game configuration and pay table are defined in code.
- The simulator does not currently expose a configurable random seed.
- Statistical validation uses a normal approximation, which is more reliable for sufficiently large simulation sizes.
- Exact symbol-outcome enumeration is practical for this small game but can become expensive as the number of reels and symbols increases.

The project is intended to demonstrate foundational mathematical concepts rather than provide a production-ready slot-game engine.

## Future Improvements

Potential improvements include:

- Support for a configurable number of reels.
- Generalized outcome representation and enumeration.
- Additional game configurations and pay tables.
- Configurable reel strips loaded from external files.
- PAR sheet generation.
- Excel or CSV export of mathematical and simulation results.
- Reproducible simulations using configurable random seeds.
- Additional statistical tests and validation methods.
- Automated comparison of different game configurations.
- Visualization of payout distributions and simulation convergence.

These enhancements would expand the project beyond its initial educational scope.

## Author

Developed by **Zachary Linger**.

- GitHub: [github.com/zblinger](https://github.com/zblinger)
- LinkedIn: [linkedin.com/in/zachary-linger](https://linkedin.com/in/zachary-linger/)

This project was developed as a practical demonstration of C# programming, probability, statistics, combinatorics, and game-mathematics simulation.