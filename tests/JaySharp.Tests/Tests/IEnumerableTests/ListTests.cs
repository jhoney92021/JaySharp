using JaySharp.Shared.Evaluations.Lists;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class ListTests
{
    [JayTest]
    public static void AreSame()
    {
        List<int> underTest = new List<int> { 1, 2, 3 };
        List<int> toCompare = new List<int> { 1, 2, 3 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_Same_Size()
    {
        List<int> underTest = new List<int> { 1, 2, 3 };
        List<int> toCompare = new List<int> { 1, 2, 6 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller()
    {
        List<int> underTest = new List<int> { 1, 2, 3 };
        List<int> toCompare = new List<int> { 1 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller_2()
    {
        List<int> underTest = new List<int> { 1, 2, 3, 5, 6 };
        List<int> toCompare = new List<int> { 1, 2, 3 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_larger()
    {
        List<int> underTest = new List<int> { 1, 2, 3 };
        List<int> toCompare = new List<int> { 1, 2, 3, 5, 6 };
        underTest.Oughta().Be(toCompare);
    }

    [JayTest]
    public static void AreNotSame_differ_by_size_and_content()
    {
        List<int> underTest = new List<int> { 4, 5, 6 };
        List<int> toCompare = new List<int> { 1, 2, 3, 9 };
        underTest.Oughta().Be(toCompare);
    }
}