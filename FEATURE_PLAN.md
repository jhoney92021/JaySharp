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
