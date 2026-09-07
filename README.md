# JaySharp #

JaySharp is a lightweight C# unit testing framework and runner offering dual application shells: a terminal **CLI** runner and a **Blazor WebAssembly PWA** dashboard interface.

---

## Repository Architecture ##

```
JaySharp/
├── src/
│   ├── JaySharp.Core/        # Core Test Runner Engine & Assertions (.NET 10)
│   ├── JaySharp.Cli/         # Console CLI Application Shell (.NET 10)
│   └── JaySharp.Web/         # Blazor WebAssembly PWA App Shell (.NET 10)
├── tests/
│   └── JaySharp.Tests/       # Internal / Example Test Suite (.NET 10)
├── Makefile                  # Task runner for Linux / macOS
├── jaysharp                  # Executable bash CLI wrapper
└── jaysharp.ps1              # Task runner script for Windows PowerShell
```

---

## Quick Start & Task Runners ##

### Linux / CachyOS / macOS (`Makefile` & `./jaysharp`)

Using `make`:
```bash
make test               # Run all test suites
make build              # Build solution (JaySharp.sln)
make version            # Display JaySharp CLI version
make run ARGS="-halp"   # Pass custom CLI flags
make pack               # Package NuGet tool to ./nupkg
make clean              # Clean build artifacts
```

Using the `./jaysharp` executable wrapper:
```bash
./jaysharp -RunTests -AllSuites -AllTests
./jaysharp -version
./jaysharp -halp
```

### Windows PowerShell (`jaysharp.ps1`)

```powershell
.\jaysharp.ps1 test               # Run all test suites
.\jaysharp.ps1 build              # Build solution (JaySharp.sln)
.\jaysharp.ps1 version            # Display JaySharp CLI version
.\jaysharp.ps1 run -halp          # Pass custom CLI flags
.\jaysharp.ps1 pack               # Package NuGet tool to ./nupkg
.\jaysharp.ps1 clean              # Clean build artifacts
```

---

## JaySharp CLI Arguments ##

When invoking `JaySharp` via `dotnet run --project src/JaySharp.Cli --`:

| Command / Flag | Description |
| :--- | :--- |
| `--JaySharp` | Base CLI entry flag |
| `-RunTests` | Triggers test execution |
| `-AllSuites` | Enables execution of all discovered test suites |
| `-AllTests` | Enables execution of all discovered test methods |
| `-AllLogs` | Sets verbose log output level |
| `-version` | Displays assembly version |
| `-halp` | Prints diagnostic / debug assembly information |

---

## Web App Shell (`JaySharp.Web`) ##

To launch the Blazor WASM PWA dashboard interface:

```bash
dotnet run --project src/JaySharp.Web
```

---

## License & Authors ##
Developed by **SpectacledJay** (jhoney).