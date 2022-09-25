using JaySharp.TestSuite.TestRunner;
using JaySharp.Shared.Loggers;
using System.Reflection;

namespace JaySharp;

class Program 
{ 
    static void Main(string[] args) 
    {
        // if(!args.Contains("JaySharp")) return;
        
        // if(args.Contains("- RunTests"))
        // {
            Settings.LogLevel = LogLevel.Verbose;
            TestRunner.GetAndRunAllTestSuites();       
        // } 
        
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


