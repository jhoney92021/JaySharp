.PHONY: all build test run version pack clean help

# Default target when running 'make'
all: build test

# Build the entire solution
build:
	dotnet build JaySharp.sln

# Run all test suites via JaySharp CLI
test:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -AllSuites -AllTests

# Run CLI with custom arguments (e.g. make run ARGS="-halp")
run:
	dotnet run --project src/JaySharp.Cli -- --JaySharp $(ARGS)

# Display JaySharp CLI version
version:
	dotnet run --project src/JaySharp.Cli -- --JaySharp -version

# Pack NuGet tool package into ./nupkg
pack:
	dotnet pack src/JaySharp.Cli -o ./nupkg

# Clean build artifacts
clean:
	dotnet clean JaySharp.sln
	rm -rf nupkg
