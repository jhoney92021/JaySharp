# Migrating to JaySharp

JaySharp is designed for **frictionless drop-in migration** from legacy testing frameworks (**xUnit**, **NUnit**, **MSTest**).

---

## Option 1: Drop-In Compatibility Aliases (Zero Code Changes)

JaySharp includes built-in attribute aliases in `JaySharp.Compatibility`. You can keep your existing `[Fact]`, `[Test]`, `[TestMethod]`, `[TestFixture]`, and `[TestClass]` attributes intact!

### 1. Attribute Mapping
| Framework | Original Attribute | JaySharp Alias (`JaySharp.Compatibility`) | Native JaySharp Attribute |
| :--- | :--- | :--- | :--- |
| **xUnit** | `[Fact]` | `[Fact]` | `[JayTest]` |
| **NUnit** | `[Test]` / `[TestFixture]` | `[Test]` / `[TestFixture]` | `[JayTest]` / `[JayTestSuite]` |
| **MSTest** | `[TestMethod]` / `[TestClass]` | `[TestMethod]` / `[TestClass]` | `[JayTest]` / `[JayTestSuite]` |

### 2. Example Drop-In Usage
```csharp
using JaySharp.Compatibility;               // Import xUnit/NUnit/MSTest attribute aliases
using JaySharp.Shared.MethodExtensions;    // Import .Must() and .Oughta() assertions

[TestClass]
public static class LegacyUnitTests
{
    [Fact]
    public static void Fact_Example() => 42.Must().Be(42);

    [Test]
    public static void NUnitTest_Example() => "JaySharp".Must().Be("JaySharp");

    [TestMethod]
    public static void MSTest_Example() => true.Must().Be(true);
}
```

---

## Option 2: 1-to-1 Find & Replace Guide

If you prefer migrating your codebase completely to native JaySharp attributes, use these 1-to-1 Find & Replace patterns across your solution:

### A. Namespace Replacements
| Find | Replace With |
| :--- | :--- |
| `using Xunit;` | `using JaySharp.TestSuite.TestAttributes;` |
| `using NUnit.Framework;` | `using JaySharp.TestSuite.TestAttributes;` |
| `using Microsoft.VisualStudio.TestTools.UnitTesting;` | `using JaySharp.TestSuite.TestAttributes;` |

### B. Attribute Replacements
| Find | Replace With |
| :--- | :--- |
| `[Fact]` | `[JayTest]` |
| `[Test]` | `[JayTest]` |
| `[TestMethod]` | `[JayTest]` |
| `[TestFixture]` | `[JayTestSuite]` |
| `[TestClass]` | `[JayTestSuite]` |

### C. Assertion Replacements
| Original Framework Assertion | JaySharp Fluent Equivalent |
| :--- | :--- |
| `Assert.Equal(expected, actual);` | `actual.Must().Be(expected);` |
| `Assert.AreEqual(expected, actual);` | `actual.Must().Be(expected);` |
| `Assert.IsTrue(condition);` | `condition.Must().Be(true);` |
| `Assert.IsFalse(condition);` | `condition.Must().Be(false);` |

---

## Migration Verification Command

After updating usings or attributes, run JaySharp from terminal or Makefile:

```bash
make test          # Run tests
make lint          # Verify code style policies
```
