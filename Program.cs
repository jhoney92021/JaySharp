using JaySharp.Tests;
namespace JaySharp;

class Program 
{ 
    static void Main(string[] args) 
    {
        Console.WriteLine("Hello World!");        
        BooleanTests.IsTrue();        
        BooleanTests.IsTrue_Fail();
        IntegerEvaluationsTests.CompareNumbers();
        IntegerEvaluationsTests.CompareNumbers_Fail();        
        IntegerEvaluationsTests.CompareNumbers_Must_Be();        
        IntegerEvaluationsTests.CompareNumbers_Must_Be_Fail();        
    }
}
