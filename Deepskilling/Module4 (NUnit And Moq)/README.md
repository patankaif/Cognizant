# Module 4 – NUnit and Moq (C#)

A two-project .NET 8 solution covering every learning objective in
**Module 4** of the DN 5.0 Deep Skilling handbook: automated testing with
NUnit, and mocking external dependencies with Moq.

## Solution layout

```
Module4NUnitAndMoq.sln
src/
  Module4.Core/                  (production code under test)
    Interfaces/
      IHistoryLogger.cs
      IDiscountService.cs
      IInventoryRepository.cs
      INotificationService.cs
    StringUtilities.cs
    ArrayUtilities.cs
    Calculator.cs                (depends on IHistoryLogger)
    OrderService.cs              (depends on 3 injected interfaces)
    PricingCalculator.cs         (has an internal method)
    FizzBuzzSolver.cs
    Properties/AssemblyInfo.cs   (InternalsVisibleTo("Module4.Tests"))
tests/
  Module4.Tests/                 (NUnit + Moq test project)
    StringUtilitiesTests.cs
    ArrayUtilitiesTests.cs
    CalculatorTests.cs
    OrderServiceTests.cs
    PricingCalculatorTests.cs
    FizzBuzzSolverTests.cs
```

## Where each learning objective is demonstrated

| Topic | File(s) |
|---|---|
| Test pyramid / TDD style (small, fast, isolated unit tests) | `FizzBuzzSolver.cs` + `FizzBuzzSolverTests.cs` |
| Parameterized tests (`[TestCase]`) | All test files |
| `[SetUp]` / `[TearDown]` | `CalculatorTests.cs`, `OrderServiceTests.cs`, `PricingCalculatorTests.cs`, `FizzBuzzSolverTests.cs` |
| Testing strings | `StringUtilitiesTests.cs` |
| Testing arrays/collections | `ArrayUtilitiesTests.cs` |
| Testing return types of methods | `CalculatorTests.cs`, `OrderServiceTests.cs` |
| Testing void methods | `CalculatorTests.cs` (`ClearHistory`, verified via mock) |
| Testing methods that throw exceptions | `CalculatorTests.cs`, `OrderServiceTests.cs`, `FizzBuzzSolverTests.cs`, `StringUtilitiesTests.cs` |
| Testing private/internal methods | `PricingCalculatorTests.cs` — shows both the `internal` + `InternalsVisibleTo` approach and a reflection-based approach |
| Dependency Injection (constructor injection) | `Calculator.cs`, `OrderService.cs` |
| Mocking frameworks / creating mocks with Moq | `CalculatorTests.cs`, `OrderServiceTests.cs` |
| State-based testing | `OrderServiceTests.PlaceOrder_SufficientStock_ReturnsCorrectTotal_StateBasedTest` |
| Interaction testing (verifying calls between objects) | `OrderServiceTests.PlaceOrder_SufficientStock_ReducesStock_InteractionTest`, `..._SendsConfirmation_InteractionTest` |

## How to run

**Option 1 – Visual Studio**
1. Open `Module4NUnitAndMoq.sln`.
2. Build the solution (this restores NuGet packages: NUnit, NUnit3TestAdapter,
   Moq, Microsoft.NET.Test.Sdk, coverlet.collector).
3. Open Test Explorer (`Test` → `Test Explorer`) and run all tests.

**Option 2 – .NET CLI**
```bash
dotnet test
```
This restores packages, builds both projects, and runs every NUnit test.

To generate a code coverage report (using the bundled `coverlet.collector`):
```bash
dotnet test --collect:"XPlat Code Coverage"
```
The resulting `coverage.cobertura.xml` can be viewed with a tool like
ReportGenerator, or opened directly in Visual Studio's Code Coverage viewer.

Requires the **.NET 8 SDK**.

## Notes

- I could not run `dotnet test` in this sandbox (no .NET SDK available), so
  please run it on your machine and let me know if any package version
  needs adjusting for your environment.
- `PricingCalculator.CalculateTax` is `internal` rather than `private` —
  this is the generally recommended way to make a method testable without
  reflection, using `[assembly: InternalsVisibleTo("Module4.Tests")]`
  (see `Properties/AssemblyInfo.cs`). The reflection-based test in
  `PricingCalculatorTests.cs` is included to show the alternative technique
  for cases where you can't change the method's accessibility.
- `OrderServiceTests` deliberately separates **state-based** tests (assert
  on the returned value) from **interaction** tests (verify a dependency
  was called correctly) so the distinction between the two testing styles
  is clear.
