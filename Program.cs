using JaySharp.Tests;
using JaySharp.TestRunner;
namespace JaySharp;

class Program 
{ 
    static void Main() 
    {
        // Console.WriteLine("Hello World!");        
        // BooleanTests.IsTrue();        
        // BooleanTests.IsTrue_Fail();
        // IntegerEvaluationsTests.CompareNumbers();
        // IntegerEvaluationsTests.CompareNumbers_Fail();        
        // IntegerEvaluationsTests.CompareNumbers_Must_Be();        
        // IntegerEvaluationsTests.CompareNumbers_Must_Be_Fail();       

        TestRunner.TestRunner.BuildTestSuites(); 
    }
}
