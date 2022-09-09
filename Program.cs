using JaySharp.TestSuite.TestRunner;
using JaySharp.Shared.Loggers;

namespace JaySharp;

class Program 
{ 
    static void Main() 
    {
        Settings.LogLevel = LogLevel.Verbose;
        TestRunner.GetAndRunAllTestSuites();         
    }
}
