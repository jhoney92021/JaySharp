using JaySharp.Shared.Evaluations.Dictionaries;
using JaySharp.TestSuite.TestAttributes;
using JaySharp.Shared.MethodExtensions;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class DictionaryTests
{
    [JayTest]
    public static void AreSame()
    {
        var underTest = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        var toCompare = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_Same_Size()
    {
        var underTest = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        var toCompare = new Dictionary<int,int>{{1,1},{2,2},{6,3}};
        underTest.Oughta().Be(toCompare);  
    }    
    [JayTest]
    public static void AreNotSame_comparison_smaller()
    {
        var underTest = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        var toCompare = new Dictionary<int,int>{{1,1}};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller_2()
    {
        var underTest = new Dictionary<int,int>{{1,1},{2,2},{3,3},{5,5},{6,6}};
        var toCompare = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        underTest.Oughta().Be(toCompare);  
    }
    [JayTest]
    public static void AreNotSame_comparison_larger()
    {
        var underTest = new Dictionary<int,int>{{1,1},{2,2},{3,3}};
        var toCompare = new Dictionary<int,int>{{1,1},{2,2},{3,3},{5,5},{6,6}};
        underTest.Oughta().Be(toCompare);
    }    

    [JayTest]
    public static void AreNotSame_differ_by_size_and_content()
    {
        var underTest = new Dictionary<int,int>{{4,4},{5,5},{6,6}};
        var toCompare = new Dictionary<int,int>{{1,1},{2,2},{3,3},{9,9}};
        underTest.Oughta().Be(toCompare);  
    }
}