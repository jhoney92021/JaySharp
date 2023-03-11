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
            // JayLogger.PrintWithColor(ASCI_Headers.JayTest_StarWars, ConsoleColor.Yellow);
            // JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_StarWars, ConsoleColor.Red);

            JayLogger.PrintWithColor(ASCI_Headers.JayTest_3D, ConsoleColor.Yellow);
            JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_3D, ConsoleColor.Red);

            // JayLogger.PrintWithColor(ASCI_Headers.JayTest_Murica, ConsoleColor.Blue);
            // JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_Murica, ConsoleColor.Red);

            // JayLogger.PrintWithColor(ASCI_Headers.JayTest_Halloween, ConsoleColor.Green);
            // JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_Halloween, ConsoleColor.Red);
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