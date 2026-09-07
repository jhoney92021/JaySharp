# JaySharp System Architecture

This document provides a comprehensive overview of the design patterns, execution models, and architectural components of **JaySharp**, a lightweight, zero-dependency C# unit testing framework and code quality toolchain.

---

## 1. Overview & Project Structure

JaySharp is structured into modular layers designed for high portability and fast execution across CLI environments, IDEs, and browser contexts (Blazor WebAssembly PWA).

```
JaySharp/
├── src/
│   ├── JaySharp.Core/            # Core engine, reflection discovery, assertions, validator
│   ├── JaySharp.Cli/             # Console entry point & CLI flag dispatcher
│   └── JaySharp.Web/             # Blazor WebAssembly PWA dashboard shell
├── samples/
│   ├── Tomodachi/                # Reference game engine domain with clean unit tests
│   └── Tomodachi.Smelly/         # Smellydachi benchmark project exhibiting code smells
├── tests/
│   └── JaySharp.Tests/           # Internal JaySharp framework test suite
├── docs/
│   └── ARCHITECTURE.md           # System architecture guide
└── FEATURE_PLAN.md               # Feature roadmap & proposal specs
```

---

## 2. Core Architectural Components

### A. Dynamic Test Discovery & Execution Engine (`JaySharp.Core`)
* **Reflection Engine (`TestRunner`):** JaySharp scans target assemblies for classes decorated with `[JayTestSuite]` and methods decorated with `[JayTest]`.
* **Execution Scoping & Filtering:**
  * **Default State:** Tests execute based on their `Is.On` or `Is.Off` state specified in attributes.
  * **Nuclear Mode (`-AllSuites -AllTests`):** Forces execution of all discovered test methods regardless of activation state.
  * **Feature Tagging (`[JayFeature("FeatureName")]`):** Selective execution mode using `-Feature <Name>` to isolate tests belonging to specific functional features.
* **Test Metadata:** Attributes support an explicit `Description` property (`[JayTest(Description = "...")]`) that is displayed alongside execution outputs.

| **Feature Tag** | `[JayFeature("Name")]` | — | — | — |
| **Parameterized Scenario** | `[JayScenario(arg1, arg2)]` | `[InlineData]` | `[TestCase]` | `[DataRow]` |
| **Showstopper Circuit Breaker** | `[JayShowstopper]` | — | — | — |

* **Priority-Ordered Execution Pipeline:**
  1. **Tier 1 (Showstopper `[JayShowstopper]`):** Critical infrastructure sanity tests. If a showstopper fails, the engine triggers a circuit breaker to halt execution immediately.
  2. **Tier 2 (Standard Hard Invariants `.Must()`):** Core domain and business rule tests.
  3. **Tier 3 (Advisory `.Oughta()`):** Soft warning checks and performance budget assertions.
* **Execution Modes:**
  * **Hotfix Mode (`-Hotfix`):** Runs Tier 1 + Tier 2 tests only, skipping low-priority advisory tests for rapid production hotfixes.
  * **Nightly Mode (`-Nightly`):** Runs all tests (`-AllSuites -AllTests`), full code policy linter (`-Lint -ExplicitVar`), and exports visual HTML & JUnit XML reports to `./reports/`.

### B. Dual-Tier Assertion Model (`Must()` vs `Oughta()`)
JaySharp introduces a two-tier evaluation model for test assertions:

| Tier | Method Chain | Failure Action | Use Case |
| :--- | :--- | :--- | :--- |
| **Hard Assertion** | `.Must().Be(...)` | Throws `EvaluationException` (fails test immediately) | Critical invariants, business rule enforcement, schema validity |
| **Soft Warning** | `.Oughta().Be(...)` | Logs yellow warning tag (`¿¿`) without halting execution | Non-fatal code smells, recommendations, performance thresholds |

### C. Static Analyzer & Junk Test Detector (`JaySharpCodeValidator`)
JaySharp includes a built-in static analysis engine to prevent low-value or "junk" tests from cluttering codebases:
1. **`AssertionLessTest`:** Identifies test methods decorated with `[JayTest]` that execute code but contain no assertions (`.Must()`, `.Oughta()`, `.IsTrue()`).
2. **`TautologicalAssertion`:** Detects useless literal self-assertions (e.g. `"A".Must().Be("A")` or `10.Oughta().Be(10)`).
3. **`DisabledTestCruft`:** Flagged when tests are explicitly turned off (`[JayTest(On = Is.Off)]`) without clean removal.
4. **`EnforceExplicitVar`:** Configurable code style policy prohibiting implicit `var` declarations in favor of strong static typing.
5. **`DeadCodeDetector`:** Scans source files for uncalled private/internal helper methods, unread fields/constants, and orphaned unreachable code paths.

### D. Tag Glyphs & Visual Output System (`Glyphes`)
All console log tags across JaySharp are centralized in `Glyphes.cs`:

* `¡¡` (`Glyphes.PassTag`) - Passed assertions & tests (Cyan)
* `¿¿` (`Glyphes.WarnTag`) - Soft `Oughta` warnings (Yellow)
* `~~` (`Glyphes.FailTag`) - Hard `Must` test failures (Red)
* `||` (`Glyphes.InfoTag`) - CLI headers & section dividers (Blue)

---

## 3. Configuration & Conventions

JaySharp uses a layered configuration system (`JaySharpConfigLoader` & `JaySharpRunner`):
1. **Fluent Setup API (`JaySharpRunner.Configure`):** Type-safe C# setup method for programmatic configuration:
```csharp
JaySharpRunner.Configure(config => config
    .WithExplicitVar()
    .WithVerboseLogging()
    .WithFileScopedNamespaces()
);
```
2. **JSON Configuration (`jaysharp.json`):** Declarative settings parsed at startup.
3. **C# Convention (`ConfigureJaySharp`):** Reflection discovers static configuration entry points in target assemblies:
```csharp
public static class TestSetup
{
    public static void ConfigureJaySharp(JaySharpConfig config)
    {
        config.EnforceExplicitVar = true;
        config.LogLevel = LogLevel.Verbose;
    }
}
```

---

## 4. IDE Tooling & XML Documentation

All public APIs, attributes, loggers, and evaluation extension methods in `JaySharp.Core` generate XML documentation files (`<GenerateDocumentationFile>true</GenerateDocumentationFile>`).

This provides instant hover tooltips, method parameter assistance, and summary documentation inside:
* Visual Studio & VS Code
* Kate Editor (via OmniSharp / Roslyn LSP)
* JetBrains Rider & Neovim LSP clients

---

## 5. Local Package Distribution & Fallback Mechanisms

JaySharp supports two local consumption strategies prior to publishing to public package feeds:

### Option A: Local NuGet Feed (`nuget.config`)
Add a `nuget.config` in your consumer project root pointing to your local package build directory:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="LocalJaySharp" value="./nupkg" />
  </packageSources>
</configuration>
```
Reference JaySharp in consumer `.csproj` files:
```xml
<ItemGroup>
  <PackageReference Include="JaySharp.Core" Version="1.0.18" />
</ItemGroup>
```

To refresh local packages during development without stale NuGet caching:
```bash
make pack-local    # Packages ./nupkg and clears local NuGet cache
```

### Option B: Manual Direct DLL Fallback
If NuGet package managers or local feeds are unavailable or restricted in offline environments, reference `JaySharp.Core.dll` directly:
```xml
<ItemGroup>
  <Reference Include="JaySharp.Core">
    <HintPath>./libs/JaySharp.Core.dll</HintPath>
  </Reference>
</ItemGroup>
```

---

## 6. DB & API Testing Helpers (`JaySharp.Testing`)

JaySharp includes lightweight, zero-dependency testing helpers to solve the #1 and #2 pain points in real-world testing (isolating database and API calls):

* **HTTP / API Mock Helper (`JayHttpClient`):** Constructs a mock `HttpClient` returning canned JSON or string responses without making live network calls:
```csharp
HttpClient client = JayHttpClient.Create("{\"name\":\"Tama\",\"hunger\":30}");
```
* **In-Memory Repository Container (`FakeRepository<TKey, TEntity>`):** Thread-safe in-memory CRUD storage for mocking database entities without live DB connections:
```csharp
FakeRepository<int, Pet> fakeDb = new();
fakeDb.Add(1, new Pet("Tama"));
```

---

## 7. Reflection Power Utilities Roadmap (`Reflect`)

JaySharp's reflection engine provides high-productivity testing utilities:

* **Automatic Dummy Data Seeder (`Reflect.CreateDummy<T>()`):** Auto-populates object graphs with typed dummy data for instant test fixture setup.
* **Private State Inspection (`Reflect.GetPrivateField`):** Inspects/sets private encapsulated fields during legacy code refactoring.
* **Auto-Interface Stubber (`Reflect.CreateStub<T>()`):** Instantiates lightweight default interface stubs.
* **Architectural Invariants (`Reflect.EnforceArchitecture()`):** Validates solution architecture rules (sealed DTOs, namespace boundaries).
