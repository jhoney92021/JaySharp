using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.TestRunner;
using System.Reflection;

namespace JaySharp;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Contains("--JayTest"))
        {
            JayLogger.PrintWithColor(ASCI_Headers.JaySharp_3D, ConsoleColor.Blue);
            JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_JaySharp_3D, ConsoleColor.Red);
            if (args.Contains("-halp"))
            {
                TestSettings.Halp = true;
                foreach (var arg in args)
                {
                    JayLogger.PrintWithColor($"~~~ {arg} ~~~", ConsoleColor.Red);
                }
            }
            if (args.Contains("-RunTests"))
            {
                if (args.Contains("-AllLogs"))
                {
                    TestSettings.LogLevel = LogLevel.Verbose;
                }

                if (args.Contains("-AllSuites"))
                {
                    TestSettings.RunAllSuites = true;
                }

                if (args.Contains("-AllTests"))
                {
                    TestSettings.RunAllTests = true;
                }
                TestRunner.GetAndRunAllTestSuites();
            }
            if (args.Contains("-version"))
            {
                var versionString = Assembly.GetEntryAssembly()?
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                        .InformationalVersion
                                        .ToString();
                Console.WriteLine($"~~~ JaySharp v{versionString} ~~~");
                return;
            }
        }
    }
}