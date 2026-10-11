# Game Math Simulator

A C# / .NET 10 portfolio project that compares exact mathematical analysis with Monte Carlo simulation for a simple three-reel slot game.

> **Starter template:** Edit the sample wording to match your implementation and voice. Replace each `TODO` before publishing, and remove this note when you are finished.

## Overview

Game Math Simulator calculates the theoretical behavior of a small slot game and compares it with results from simulated spins. It reports return to player (RTP), hit frequency, variance, standard deviation, and sampling uncertainty.

I built this project to practice probability, statistics, combinatorics, and C# development in the context of game mathematics.

**TODO:** Add a few sentences about your motivation, what you learned, and the parts you implemented yourself.

### Demonstration scope

This is an educational demonstration, not a commercially balanced slot game. Its theoretical RTP of **110.4% is intentionally unbalanced for demonstration purposes**: the expected payout exceeds the wager.

**TODO:** Describe the current scope and any limitations of your implementation.

## Features

- Reel and symbol models with a shared test-game configuration.
- Pay table and payout calculations.
- Exact enumeration of all reel-stop combinations.
- Theoretical RTP, hit frequency, variance, and standard deviation.
- Monte Carlo simulation and comparison with theoretical results.
- Standard error and an approximate 95% sampling range for simulated RTP.
- Console reporting.
- Automated tests: **11 passing tests** in the latest reported run.

**TODO:** Confirm this list matches the final code. Add or remove features as needed.

## Game Configuration

The example game has three reels, each with five stops. Each spin selects one stop independently and uniformly from each reel. The model below assumes a single evaluated line and a **one-unit wager per spin**.

**TODO:** Confirm these assumptions match the implementation, and explain how your code selects stops and evaluates wins.

| Stop | Reel 1 | Reel 2 | Reel 3 |
|---|---|---|---|
| 1 | Cherry | Cherry | Cherry |
| 2 | Cherry | Lemon | Cherry |
| 3 | Lemon | Lemon | Cherry |
| 4 | Bell | Bell | Bell |
| 5 | Seven | Seven | Seven |

There are `5 × 5 × 5 = 125` equally likely stop combinations. Repeated symbols occupy separate stops and therefore affect outcome probabilities.

### Pay Table

Payouts below are total returned units for a one-unit wager. All other symbol combinations pay zero.

| Combination | Payout | Winning stop combinations | Probability |
|---|---:|---:|---:|
| Cherry–Cherry–Cherry (CCC) | 3 | 6 | 4.8% |
| Lemon–Lemon–Lemon (LLL) | 5 | 0 | 0% |
| Bell–Bell–Bell (BBB) | 20 | 1 | 0.8% |
| Seven–Seven–Seven (777) | 100 | 1 | 0.8% |

**LLL is impossible in this configuration** because Reel 3 contains no Lemon stops. The pay-table entry exists but cannot occur with these reel strips.

**TODO:** Link to the source files that define the reels and pay table, using repository-relative links.

## Mathematical Model

Let `X` be the total payout from one spin, measured in wager units. With a one-unit wager, the expected payout is also RTP expressed as a decimal.

### Expected Payout and RTP

```text
E[X] = Σ (payout × probability)
     = 3 × (6/125) + 5 × (0/125) + 20 × (1/125) + 100 × (1/125)
     = 1.104

Theoretical RTP = 1.104 × 100% = 110.4%
```

This means an average return of 1.104 units per unit wagered over the mathematical distribution. It does not guarantee a particular return in a finite simulation.

### Hit Frequency

A hit is a spin with a positive payout.

```text
Hit frequency = P(X > 0) = (6 + 1 + 1) / 125 = 6.4%
```

### Variance and Standard Deviation

```text
E[X²] = 3² × (6/125) + 20² × (1/125) + 100² × (1/125)
      = 83.632

Var(X) = E[X²] − E[X]²
       = 83.632 − 1.104²
       = 82.413184

SD(X) = √Var(X) ≈ 9.078171
```

Variance measures the spread of individual payouts in squared wager units. Standard deviation describes that spread in wager units.

### Standard Error and Approximate 95% Sampling Range

For `n` independent simulated spins, the theoretical standard error of the mean payout is:

```text
SE = SD(X) / √n

Approximate 95% sampling range for simulated RTP:
    theoretical RTP ± 1.96 × SE × 100 percentage points
```

For example, with **1,000,000 spins**:

```text
SE ≈ 0.009078171 wager units
RTP standard error ≈ 0.907817 percentage points
Approximate 95% sampling range ≈ 108.6207% to 112.1793%
```

This range is centered on the theoretical RTP and describes where the simulated estimate is expected to fall approximately 95% of the time over repeated sufficiently large runs, under the model assumptions. It uses a normal approximation; it is not a guaranteed bound, and small samples can behave differently.

**TODO:** Confirm whether your code uses theoretical or estimated standard deviation, how it calculates the range, and what spin count it uses. Adjust this explanation to match.

## Example Console Output

The following is an **illustrative format**, not captured output from the application. Simulation values vary between runs.

```text
Game Math Simulator v1.0

Exact analysis
  Total stop combinations:          125
  Theoretical RTP:                  110.4000%
  Hit frequency:                    6.4000%
  Variance:                         82.413184
  Standard deviation:               9.078171

Simulation
  Spins:                            1,000,000
  Simulated RTP:                    [TODO: actual result]
  Simulated hit frequency:          [TODO: actual result]
  Standard error of mean payout:    0.009078171
  Approximate 95% RTP range:        108.6207%–112.1793%
  Validation result:                [TODO: actual result]
```

**TODO:** Replace this block with real output from your final application. Keep the spin count, standard error, and sampling range consistent with that run.

## Getting Started

### Requirements

- .NET 10 SDK.
- A terminal and an editor or IDE that supports C#.

**TODO:** Add any other requirements and confirm the project targets .NET 10.

### Run the Application

From the repository root, replace the placeholder with the actual application project path:

```sh
dotnet run --project "<path/to/application.csproj>"
```

**TODO:** Add repository clone instructions if useful. Explain where to change the number of simulated spins and whether a random seed can be configured.

### Run the Tests

From the repository root, replace the placeholder with the actual test project or solution path:

```sh
dotnet test "<path/to/tests.csproj-or-solution.sln>"
```

The latest reported test run completed with **11 tests passed, 0 failed, and 0 skipped**.

**TODO:** Update this count after your final changes and describe the behaviors the tests actually cover.

## Validation and Limitations

The project compares simulated results with exact mathematical expectations. This provides a useful consistency check, but **validation is not proof of correctness**. A simulation can fall outside an approximate 95% range by chance, and a result inside the range does not establish that the implementation is correct. Exact analysis and simulation may also share an error if they rely on the same incorrect logic.

Automated tests and independently calculated expectations help check specific behavior. Passing all 11 tests confirms only the cases those tests cover.

**TODO:** Explain your validation rule, any independently calculated checks, and known limitations. Avoid describing a statistical check as a guaranteed pass/fail test of correctness.

## Future Improvements

Possible next steps include:

- Configurable reel counts and reel strips.
- Additional pay tables and game configurations.
- PAR sheet generation.
- Excel or CSV export.
- Configurable random seeds for reproducible simulation runs.
- More independently calculated test cases.

**TODO:** Keep only improvements you are interested in pursuing and add your own priorities.

## Author

**TODO:** Add your name and, if desired, links to your portfolio or professional profile.

**TODO:** Choose a license before adding a license statement. Remove this section if you prefer to keep author details elsewhere.
