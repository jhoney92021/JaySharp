# 🚀 JaySharp Feature Roadmap & Plan

This document outlines planned features and architectural enhancements for JaySharp—a defensive programming, assertion, and testing library for C#.

---

## 🎯 Priority Feature 1: Junk Test & Anti-Cruft Detector (`JunkTestDetector`)

### Problem
In large codebases, developers often write "junk tests"—test methods decorated with `[JayTest]` that execute code but make no actual assertions, compare trivial hardcoded literals, or remain disabled indefinitely.

### Solution
An automated linter and runtime analyzer that detects:
1. **Assertion-Less Tests**: Methods marked with `[JayTest]` that complete execution without invoking any `.Must()`, `.Oughta()`, or evaluation method.
2. **Tautological / Trivial Assertions**: Static analysis detecting comparisons of identical literal constants (e.g. `1.Oughta().Be(1)` or `"a".Must().Be("a")`).
3. **Disabled Test Cruft**: Tracking test methods permanently marked `[JayTest(On = Is.Off)]` or disabled features.

### Command / API Integration
- `jaysharp -Lint -DetectJunkTests`
- `make lint-junk`

---

## ⏱️ Feature 2: Performance Budget Assertions (`[JayBenchmark]`, `CompleteWithin`)

### Capabilities
- **Method Execution Budget**: `(action).Oughta().CompleteWithin(TimeSpan.FromMilliseconds(50))`
- **Attribute Budget**: `[JayTest(MaxDurationMs = 100)]`
- Automatically logs warning or fails assertion if execution duration exceeds target performance threshold.

---

## 🔍 Feature 3: Structural Diff Visualizer for Failed Collections & Objects

### Capabilities
- When a `List`, `Dictionary`, `Array`, or complex `Type` comparison fails, format a side-by-side **Glyphed Delta View** in terminal output:
```text
¿¿ Difference Detected:
   - Expected Key [2]: Value = 20
   + Actual   Key [2]: Value = 99
??
```

---

## 🔄 Feature 4: Live Watcher Mode (`jaysharp watch` / `make watch`)

### Capabilities
- Add a file-system watcher CLI option (`jaysharp -Watch` or `make watch`).
- Monitors `.cs` file saves in your solution and automatically re-runs modified test suites and code policy linters in real-time.

---

## 📄 Feature 5: Markdown & HTML CI/CD Test & Policy Report Generator

### Capabilities
- `jaysharp -Report html` or `jaysharp -Report markdown`
- Generates a standalone `TestReport.html` or `TestReport.md` containing collapsible test suite breakdowns, pass/fail metrics, and code policy compliance scores.

---

## 🛡️ Feature 6: Fluent Defensive Guard Chains

### Capabilities
- Expand assertion methods for common defensive programming scenarios:
  - `toEvaluate.Must().NotBeNull()`
  - `number.Must().BeBetween(min, max)`
  - `text.Oughta().StartWith("http")`
  - Fluent Chaining: `value.Must().NotBeNull().And().BeGreaterThan(0)`

---

## ⚙️ Feature 7: GitHub Actions CI Generator (`jaysharp --init-ci`)

### Capabilities
- Command `jaysharp --init-ci` that generates `.github/workflows/jaysharp.yml` pre-configured to run `make build`, `make enforce`, and `make test` on every pull request.

---

## 📋 Feature 8: Feature Gap & Migration Roadmap Audit

### 1. `async Task` Test Execution (Completed)
- **Status:** Integrated into `JaySharp.Core`.
- **Capability:** `TestRunner` automatically awaits `Task` and `ValueTask` return types (`task.GetAwaiter().GetResult()`) ensuring unhandled async exceptions are caught as test failures.

### 2. Parameterized Scenario Tests (`[JayScenario]`)
- **Status:** Roadmap Proposal.
- **Capability:** Add `[JayScenario(...)]` attribute (with `[InlineData]`, `[TestCase]`, and `[DataRow]` compatibility aliases) allowing developers to annotate test methods with scenario argument rows:
```csharp
[JayTest(Description = "Validates addition across test scenarios.")]
[JayScenario(10, 20, 30)]
[JayScenario(5, 5, 10)]
public static void Add_CalculatesSum(int a, int b, int expected) => (a + b).Must().Be(expected);
```
- **Workaround:** Iterate over data arrays inside `[JayTest]` methods.

### 3. Lifecycle Setup / Teardown Hooks (`[JayBeforeEach]` / `[JayAfterEach]`)
- **Status:** Roadmap Proposal.
- **Capability:** Per-test lifecycle attributes (`[JayBeforeEach]` and `[JayAfterEach]`) executing before and after test methods in a suite.

### 4. CI Test Report Export (JUnit XML / TRX / HTML)
- **Status:** Roadmap Proposal.
- **Capability:** `-Export junit.xml` CLI flag to render test results natively in GitHub Actions / Azure DevOps build tabs.

### 5. Parallel Test Suite Execution (`-Parallel`)
- **Status:** Roadmap Proposal.
- **Capability:** Optional `-Parallel` flag for multi-threaded concurrent suite execution.

### 6. Priority-Ordered Execution & Circuit Breaker (`[JayShowstopper]`)
- **Status:** Roadmap Proposal.
- **Capability:** `[JayShowstopper]` attribute for critical infrastructure/sanity tests. Test runner executes Tier 1 (`[JayShowstopper]`) tests first, Tier 2 (`.Must()`) tests second, and Tier 3 (`.Oughta()`) tests last. If a showstopper fails, the circuit breaker halts execution immediately to prevent cascading error noise.

### 7. Emergency Hotfix Execution Mode (`-Hotfix`)
- **Status:** Roadmap Proposal.
- **Capability:** CLI flag `jaysharp -Hotfix` / `make test-hotfix` that executes only Tier 1 (Showstopper) and Tier 2 (Hard Invariants) tests, skipping low-priority advisory tests for rapid verification during production hotfixes.

### 8. Thorough Nightly Build & Deep Reporting Mode (`-Nightly` / `make test-nightly`)
- **Status:** Roadmap Proposal.
- **Capability:** Single-command execution flag `jaysharp -Nightly` / `make test-nightly` that runs nuclear test execution (`-AllSuites -AllTests`), strict code policy linter (`-Lint -ExplicitVar`), and automatically exports standalone visual HTML reports (`./reports/nightly-report.html`) and JUnit XML files (`./reports/junit.xml`) for CI/CD artifact publication.

### 9. Dead & Ghost Code Detector (`-DetectDeadCode`)
- **Status:** Roadmap Proposal.
- **Capability:** Static code analysis option `jaysharp -Lint -DetectDeadCode` that scans C# source files for unused private/internal helper methods, unread private fields/constants, and orphaned unreachable code blocks.

### 10. Automatic Dummy Data Seeder (`Reflect.CreateDummy<T>()`)
- **Status:** Roadmap Proposal.
- **Capability:** Reflextion-based object generator that inspects target DTOs or domain classes and automatically populates public properties with typed dummy data (strings, GUIDs, dates, numbers, enums, lists) for rapid test setup:
```csharp
PetDto pet = Reflect.CreateDummy<PetDto>(); // Auto-populates all properties
```

### 11. Private State Inspector & Mutator (`Reflect.GetPrivateField` / `SetPrivateField`)
- **Status:** Roadmap Proposal.
- **Capability:** Allows inspecting or setting private/internal encapsulated fields during legacy code refactoring without breaking encapsulation or polluting domain models:
```csharp
int internalState = Reflect.GetPrivateField<int>(pet, "_internalState");
```

### 12. Auto-Interface Stubber (`Reflect.CreateStub<T>()`)
- **Status:** Roadmap Proposal.
- **Capability:** Dynamically instantiates default interface stubs returning default values (`0`, empty strings, empty lists, `Task.CompletedTask`) without manual fake classes.

### 13. Architectural Invariant Enforcer (`Reflect.EnforceArchitecture()`)
- **Status:** Roadmap Proposal.
- **Capability:** Reflects over solution assemblies to enforce architectural boundaries (e.g. all DTOs must be `sealed`, no direct DB drivers in UI assemblies).

