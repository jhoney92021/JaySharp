# PowerShell Initial Setup Script for JaySharp
$ErrorActionPreference = "Stop"

Write-Host "|| ===========================================" -ForegroundColor Cyan
Write-Host "||   JaySharp Initial Environment Setup       " -ForegroundColor Cyan
Write-Host "|| ===========================================" -ForegroundColor Cyan

# 1. Check for dotnet SDK
if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host "~~ Error: dotnet SDK is not installed or not in PATH." -ForegroundColor Red
    Write-Host "Please install the .NET 10 SDK: https://dotnet.microsoft.com/download"
    Exit 1
}

$dotnetVersion = dotnet --version
Write-Host "¡¡ Found .NET SDK version: $dotnetVersion" -ForegroundColor Green

# 2. Restore dependencies & build solution
Write-Host "|| Restoring dependencies..." -ForegroundColor Cyan
dotnet restore JaySharp.sln

Write-Host "|| Building JaySharp solution..." -ForegroundColor Cyan
dotnet build JaySharp.sln -c Debug

# 3. Create NuGet tool package
Write-Host "|| Packaging JaySharp CLI NuGet tool..." -ForegroundColor Cyan
if (-not (Test-Path "./nupkg")) {
    New-Item -ItemType Directory -Path "./nupkg" | Out-Null
}
dotnet pack src/JaySharp.Cli/JaySharp.Cli.csproj -o ./nupkg -c Release

# 4. Local tool installation check
Write-Host "|| Checking local tool installation..." -ForegroundColor Cyan
$globalTools = dotnet tool list -g
if ($globalTools -match "jaysharp.cli") {
    Write-Host "¿¿ Updating existing global JaySharp.Cli tool..." -ForegroundColor Yellow
    dotnet tool update -g --add-source ./nupkg JaySharp.Cli --prerelease
} else {
    Write-Host "¡¡ Installing global JaySharp.Cli tool..." -ForegroundColor Green
    dotnet tool install -g --add-source ./nupkg JaySharp.Cli --prerelease
}

# 5. Run sanity test suite
Write-Host "|| Running initial sanity test suite..." -ForegroundColor Cyan
.\jaysharp.ps1 test

Write-Host "¡¡ Setup complete! You can run tests using '.\jaysharp.ps1 test'." -ForegroundColor Green
