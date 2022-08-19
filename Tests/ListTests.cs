using JaySharp.Evaluations.Lists;
using JaySharp.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite]
public static class ListTests
{
    [JayTest]
    public static void AreSame()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1,2,3};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1,2,6};
        underTest.Oughta().Be(toCompare);  
    }    
    [JayTest]
    public static void AreNotSame_1()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_2()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1,2,3,5,6};
        underTest.Oughta().Be(toCompare);  
    }    
    [JayTest]
    public static void AreNotSame_3()
    {
        var underTest = new List<int>{1,2,3,5,6};
        var toCompare = new List<int>{1,2,3};
        underTest.Oughta().Be(toCompare);  
    }
}