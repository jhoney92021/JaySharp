using JaySharp.Shared.Evaluations.Lists;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
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
    public static void AreNotSame_Same_Size()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1,2,6};
        underTest.Oughta().Be(toCompare);  
    }    
    [JayTest]
    public static void AreNotSame_comparison_smaller()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller_2()
    {
        var underTest = new List<int>{1,2,3,5,6};
        var toCompare = new List<int>{1,2,3};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_comparison_larger()
    {
        var underTest = new List<int>{1,2,3};
        var toCompare = new List<int>{1,2,3,5,6};
        underTest.Oughta().Be(toCompare);  
    }    

    [JayTest]
    public static void AreNotSame_differ_by_size_and_content()
    {
        var underTest = new List<int>{4,5,6};
        var toCompare = new List<int>{1,2,3,9};
        underTest.Oughta().Be(toCompare);  
    }
}