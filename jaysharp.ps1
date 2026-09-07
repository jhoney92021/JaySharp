param(
    [Parameter(Position=0)]
    [string]$Task = "test",

    [Parameter(Position=1, ValueFromRemainingArguments=$true)]
    [string[]]$CustomArgs
)

switch ($Task.ToLower()) {
    "setup"        { .\initial-setup.ps1 }
    "build"        { dotnet build JaySharp.sln }
    "test"         { dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests }
    "test-all"     { dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -AllSuites -AllTests }
    "test-feature" { dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -Feature $CustomArgs }
    "lint"         { dotnet run --project src/JaySharp.Cli -- --JaySharp -Lint }
    "enforce"      { dotnet run --project src/JaySharp.Cli -- --JaySharp -Lint -ExplicitVar }
    "run"          { dotnet run --project src/JaySharp.Cli -- --JaySharp $CustomArgs }
    "version"      { dotnet run --project src/JaySharp.Cli -- --JaySharp -version }
    "pack"         {
        dotnet pack src/JaySharp.Core/JaySharp.Core.csproj -o ./nupkg
        dotnet pack src/JaySharp.Cli/JaySharp.Cli.csproj -o ./nupkg
    }
    "pack-local"   {
        dotnet pack src/JaySharp.Core/JaySharp.Core.csproj -o ./nupkg
        dotnet pack src/JaySharp.Cli/JaySharp.Cli.csproj -o ./nupkg
        dotnet nuget locals http-cache --clear
        dotnet nuget locals temp --clear
    }
    "clean"        {
        dotnet clean JaySharp.sln
        if (Test-Path ./nupkg) { Remove-Item -Recurse -Force ./nupkg }
    }
    default        {
        Write-Host "Usage: .\jaysharp.ps1 [setup|build|test|test-all|test-feature <Name>|lint|enforce|run|version|pack|pack-local|clean]" -ForegroundColor Yellow
    }
}
