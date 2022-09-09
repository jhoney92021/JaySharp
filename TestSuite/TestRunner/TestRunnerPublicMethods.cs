namespace JaySharp.TestSuite.TestRunner;
public static partial class TestRunner
{
    public static void GetAndRunAllTestSuites()
    {
        GetTestSuites(); 
        GetTests(); 
        RunTests(); 
    }
}