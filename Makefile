.PHONY: all setup build test test-all test-feature sample-tomodachi sample-smellydachi lint enforce run version pack clean help

# Default target when running 'make'
all: build test

# Run initial setup script for Linux/macOS
setup:
	./initial-setup.sh

# Build the entire solution
build:
	dotnet build JaySharp.sln

# Run active test suites (On = Is.On) - Clean Output
test:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests

# Run ALL test suites & methods (Nuclear Option)
test-all:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -AllSuites -AllTests

# Run tests for a specific feature (e.g. make test-feature FEATURE=ConfigFallback)
test-feature:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -Feature $(FEATURE)

# Run Tomodachi sample application
sample-tomodachi:
	dotnet run --project samples/Tomodachi

# Run Smellydachi anti-pattern sample application
sample-smellydachi:
	dotnet run --project samples/Tomodachi.Smelly

# Run CLI with custom arguments (e.g. make run ARGS="-halp")
run:
	dotnet run --project src/JaySharp.Cli -- --JaySharp $(ARGS)

# Display JaySharp CLI version
version:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -version

# Run code policy linter using current jaysharp.json config
lint:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -Lint

# Run code policy linter strictly enforcing explicit var declarations
enforce:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -Lint -ExplicitVar

# Pack NuGet packages into ./nupkg
pack:
	dotnet pack src/JaySharp.Core/JaySharp.Core.csproj -o ./nupkg
	dotnet pack src/JaySharp.Cli/JaySharp.Cli.csproj -o ./nupkg

# Pack NuGet packages into ./nupkg and clear local HTTP/temp cache to prevent stale builds
pack-local: pack
	dotnet nuget locals http-cache --clear
	dotnet nuget locals temp --clear

# Clean build artifacts & restore state
clean:
	dotnet clean JaySharp.sln 2>/dev/null || true
	rm -rf nupkg
	find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null || true
