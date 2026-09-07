#!/usr/bin/env bash
set -e

GREEN='\033[0;32m'
CYAN='\033[0;36m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${CYAN}|| ==========================================="${NC}
echo -e "${CYAN}||   JaySharp Initial Environment Setup       "${NC}
echo -e "${CYAN}|| ==========================================="${NC}

# 1. Check for dotnet SDK
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}~~ Error: dotnet SDK is not installed or not in PATH.${NC}"
    echo -e "Please install the .NET 10 SDK: https://dotnet.microsoft.com/download"
    exit 1
fi

DOTNET_VERSION=$(dotnet --version)
echo -e "${GREEN}¡¡ Found .NET SDK version: ${DOTNET_VERSION}${NC}"

# 2. Make shell scripts executable
echo -e "${CYAN}|| Setting executable permissions on scripts...${NC}"
chmod +x ./jaysharp ./initial-setup.sh 2>/dev/null || true

# 3. Restore dependencies & build solution
echo -e "${CYAN}|| Restoring dependencies...${NC}"
dotnet restore JaySharp.sln

echo -e "${CYAN}|| Building JaySharp solution...${NC}"
dotnet build JaySharp.sln -c Debug

# 4. Create NuGet tool package
echo -e "${CYAN}|| Packaging JaySharp CLI NuGet tool...${NC}"
mkdir -p ./nupkg
dotnet pack src/JaySharp.Cli/JaySharp.Cli.csproj -o ./nupkg -c Release

# 5. Local tool installation check
echo -e "${CYAN}|| Checking local tool installation...${NC}"
if dotnet tool list -g | grep -q "jaysharp.cli"; then
    echo -e "${YELLOW}¿¿ Updating existing global JaySharp.Cli tool...${NC}"
    dotnet tool update -g --add-source ./nupkg JaySharp.Cli --prerelease || true
else
    echo -e "${GREEN}¡¡ Installing global JaySharp.Cli tool...${NC}"
    dotnet tool install -g --add-source ./nupkg JaySharp.Cli --prerelease || true
fi

# 6. Run sanity test suite
echo -e "${CYAN}|| Running initial sanity test suite...${NC}"
./jaysharp -RunTests

echo -e "${GREEN}¡¡ Setup complete! You can run tests using './jaysharp -RunTests' or 'make test'.${NC}"
