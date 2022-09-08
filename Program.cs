using JaySharp.Tests;
using JaySharp.TestRunner;
using JaySharp.Loggers;

namespace JaySharp;

class Program 
{ 
    static void Main() 
    {
        Settings.LogLevel = LogLevel.Verbose;
        TestRunner.TestRunner.GetAndRunAllTestSuites();         
    }
}
