using JaySharp.TestSuite.TestRunner;
using JaySharp.Shared.Loggers;
using JaySharp.CommandLineArguments;

namespace JaySharp;

class Program 
{ 
    static void Main(string[] args) 
    {        
        if(args.Contains("--JayTest")) 
        {
            JayLogger.PrintWithColor(ASCI_Headers.JayTest_StarWars, ConsoleColor.Yellow);
            JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies, ConsoleColor.Red);            
        }
        

        if(args.Contains("-RunTests"))
        {
            if(args.Contains("-AllLogs"))
            {
                TestSettings.LogLevel = LogLevel.Verbose;
            }
            
            if(args.Contains("-AllSuites"))
            {
                TestSettings.RunAllSuites = true;            
            }
            
            if(args.Contains("-AllTests"))
            {
                TestSettings.RunAllTests = true;
            }
            TestRunner.GetAndRunAllTestSuites();       
        } 
        
        // if (args.Contains("version"))
        // {
        //     var versionString = Assembly.GetEntryAssembly()?
        //                             .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        //                             .InformationalVersion
        //                             .ToString();

        //     Console.WriteLine($"~~~ JaySharp v{versionString} ~~~");
        //     return;
        // }
    }
}