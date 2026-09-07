param(
    [Parameter(Position=0)]
    [string]$Task = "test",

    [Parameter(Position=1, ValueFromRemainingArguments=$true)]
    [string[]]$CustomArgs
)

switch ($Task.ToLower()) {
    "build"   { dotnet build JaySharp.sln }
    "test"    { dotnet run --project src/JaySharp.Cli -- --JaySharp -RunTests -AllSuites -AllTests }
    "run"     { dotnet run --project src/JaySharp.Cli -- --JaySharp $CustomArgs }
    "version" { dotnet run --project src/JaySharp.Cli -- --JaySharp -version }
    "pack"    { dotnet pack src/JaySharp.Cli -o ./nupkg }
    "clean"   {
        dotnet clean JaySharp.sln
        if (Test-Path ./nupkg) { Remove-Item -Recurse -Force ./nupkg }
    }
    default   {
        Write-Host "Usage: .\jaysharp.ps1 [build|test|run|version|pack|clean]" -ForegroundColor Yellow
    }
}
