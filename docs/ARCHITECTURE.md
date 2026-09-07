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
