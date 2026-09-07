# JaySharp #

JaySharp is a lightweight C# unit testing framework, static code analyzer, and test runner offering dual application shells: a terminal **CLI** runner and a **Blazor WebAssembly PWA** dashboard interface.

---

## Repository Architecture ##

```
JaySharp/
├── src/
│   ├── JaySharp.Core/        # Core Engine, Assertions, Static Validator (.NET 10)
│   ├── JaySharp.Cli/         # Console CLI Application Shell (.NET 10)
│   └── JaySharp.Web/         # Blazor WebAssembly PWA Dashboard Shell (.NET 10)
├── samples/
│   ├── Tomodachi/            # Virtual pet simulator sample project
│   └── Tomodachi.Smelly/     # "Smellydachi" code smells & junk test benchmark project
├── tests/
│   └── JaySharp.Tests/       # Internal JaySharp test suite (.NET 10)
├── docs/
│   └── ARCHITECTURE.md       # Detailed system design & component documentation
├── FEATURE_PLAN.md           # Roadmap and feature expansion specs
├── Makefile                  # Linux / macOS task runner
├── jaysharp                  # Bash CLI wrapper script
└── jaysharp.ps1              # PowerShell task runner script
```

---

## Quick Start & Environment Setup ##

### Initial Environment Setup

* **Linux / macOS:** Run `./initial-setup.sh` or `make setup`
* **Windows PowerShell:** Run `.\initial-setup.ps1` or `.\jaysharp.ps1 setup`

The initial setup scripts check for .NET SDK 10, restore dependencies, build the solution, package the `JaySharp.Cli` NuGet tool to `./nupkg`, install/update the local global CLI tool, and run initial test suite verification.

### Linux / CachyOS / macOS (`Makefile` & `./jaysharp`)

Using `make`:
```bash
make setup                                     # Run initial setup script
make test                                      # Run active test suites (On = Is.On)
make test-all                                  # Run ALL test suites & methods (Nuclear option)
make test-feature FEATURE=ConfigFallback       # Target tests tagged with [JayFeature("ConfigFallback")]
make sample-tomodachi                          # Run Tomodachi reference sample tests
make sample-smellydachi                        # Run Smellydachi code smell analyzer benchmark
make lint                                      # Analyze project files with JaySharp linter
make build                                     # Build solution (JaySharp.sln)
make version                                   # Display JaySharp CLI version
make run ARGS="-halp"                          # Pass custom CLI flags
make pack                                      # Package NuGet tools & library to ./nupkg
make pack-local                                # Package NuGet tools & clear local cache
make clean                                     # Clean build artifacts
```

Using the `./jaysharp` executable wrapper:
```bash
./initial-setup.sh                             # Environment setup & initial test check
./jaysharp -RunTests                           # Run active tests
./jaysharp -RunTests -Feature ConfigFallback   # Target specific feature
./jaysharp -RunTests -AllSuites -AllTests      # Run all tests
./jaysharp -Lint                               # Execute code quality & junk test detector
./jaysharp -version
./jaysharp -halp
```

### Windows PowerShell (`jaysharp.ps1`)

```powershell
.\jaysharp.ps1 setup                           # Run initial environment setup
.\jaysharp.ps1 test                            # Run active test suites
.\jaysharp.ps1 test-all                        # Run ALL test suites & methods
.\jaysharp.ps1 test-feature ConfigFallback     # Target specific feature
.\jaysharp.ps1 build                           # Build solution (JaySharp.sln)
.\jaysharp.ps1 version                         # Display JaySharp CLI version
.\jaysharp.ps1 run -halp                       # Pass custom CLI flags
.\jaysharp.ps1 pack                            # Package NuGet tool to ./nupkg
.\jaysharp.ps1 clean                           # Clean build artifacts
```

---

## Key Features & Testing Syntax ##

### 1. Dual Assertion Model: `Must()` vs `Oughta()`
JaySharp provides two levels of assertions:
* **`Must()` (Hard Assertions):** Fails immediately and halts the test execution when expectations are violated.
* **`Oughta()` (Soft Assertions):** Logs a yellow warning (`¿¿`) without halting execution—ideal for non-critical invariants or performance budgets.

```csharp
pet.Hunger.Must().Be(0);             // Hard assertion (fails test if false)
pet.Happiness.Oughta().BeGreaterThan(50); // Soft assertion (logs warning if false)
```

### 2. Embedded Descriptions & Metadata
Add test metadata directly to the test attributes without cluttering unit test bodies with comments:

```csharp
[JayTestSuite(Description = "Validates virtual pet stat decay and feeding rules.")]
public static class PetEngineTests
{
    [JayTest(Description = "Feeding pet should decrease hunger level to zero.")]
    public static void Feed_ClearsHunger()
    {
        Pet pet = new Pet("Tama");
        pet.Feed();
        pet.Hunger.Must().Be(0);
    }
}
```

### 3. Feature-Based Tagging
Tag suites or individual tests with `[JayFeature("FeatureName")]` for granular test runs:

```csharp
[JayTestSuite]
[JayFeature("MiniGames")]
public static class GameLogicTests
{
    [JayTest]
    public static void PlayGame_IncreasesHappiness()
    {
        // ...
    }
}
```

Run targeted tests via `make test-feature FEATURE=MiniGames`.

### 4. Code Quality & Junk Test Linter (`-Lint`)
JaySharp detects low-value test practices ("junk tests") and code style violations:
* **`AssertionLessTest`:** Identifies tests without any `.Must()`, `.Oughta()`, or `.IsTrue()` assertions.
* **`TautologicalAssertion`:** Catches self-matching assertions like `"Tama".Must().Be("Tama")`.
* **`DisabledTestCruft`:** Flags turned-off tests (`Is.Off`).
* **`EnforceExplicitVar`:** Optional rule enforcing explicit types instead of implicit `var`.

To run the linter:
```bash
dotnet run --project src/JaySharp.Cli -- -Lint
```

---

## JaySharp CLI Flag Reference ##

When invoking `JaySharp` CLI:

| Command / Flag | Description |
| :--- | :--- |
| `--JaySharp` | Base CLI entry flag |
| `-RunTests` | Triggers test execution |
| `-Feature <Name>` | Filters execution to tests matching `[JayFeature("Name")]` |
| `-AllSuites` | Enables execution of all discovered test suites |
| `-AllTests` | Enables execution of all discovered test methods |
| `-Lint` | Runs the static code quality & junk test detector |
| `-ExplicitVar` | Enforces explicit type declarations over `var` during linting |
| `-DetectDeadCode` | Analyzes assembly source files for uncalled helper methods and unread fields |
| `-Hotfix` | Runs Tier 1 (Showstoppers) + Tier 2 (Hard Invariants) tests, skipping low-priority advisory tests |
| `-Showstoppers` | Runs Tier 1 critical infrastructure sanity tests only |
| `-Nightly` | Deep execution mode running all tests, full linter audit, and exporting HTML/JUnit reports |
| `-AllLogs` | Sets verbose log output level |
| `-version` | Displays assembly version |
| `-halp` | Prints diagnostic / debug assembly information |

---

## Framework Compatibility & Migration ##

JaySharp provides drop-in attribute aliases (`JaySharp.Compatibility`) for developers migrating from **xUnit**, **NUnit**, or **MSTest**:

```csharp
using JaySharp.Compatibility;               // Aliases [Fact], [Test], [TestMethod], [TestFixture], [TestClass]
using JaySharp.Shared.MethodExtensions;    // Assertions .Must() and .Oughta()

[TestClass]
public static class LegacyUnitTests
{
    [Fact]
    public static void xUnitTest() => 42.Must().Be(42);

    [Test]
    public static void NUnitTest() => "Tama".Must().Be("Tama");

    [TestMethod]
    public static void MSTest() => true.Must().Be(true);
}
```

See [docs/MIGRATION.md](file:///home/jay/git/sideprojects/JaySharp/docs/MIGRATION.md) for 1-to-1 Find & Replace rules.

---

## IDE Support & XML Documentation ##

`JaySharp.Core` emits full XML documentation (`JaySharp.Core.xml`), supplying hover tooltips, method signatures, and parameter guidance across:
* Visual Studio & VS Code
* Kate Editor (via OmniSharp / Roslyn LSP)
* Rider & Neovim LSP setups

---

## Web App Shell (`JaySharp.Web`) ##

To launch the Blazor WASM PWA dashboard interface:

```bash
dotnet run --project src/JaySharp.Web
```

---

## License & Authors ##
Developed by **SpectacledJay** (jhoney). Licensed under the [MIT License](file:///home/jay/git/sideprojects/JaySharp/LICENSE).