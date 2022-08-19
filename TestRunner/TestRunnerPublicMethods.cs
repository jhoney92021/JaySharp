namespace JaySharp.TestRunner;
public static partial class TestRunner
{
    public static void GetAndRunAllTestSuites()
    {
        GetTestSuites(); 
        GetTests(); 
        RunTests(); 
    }
}