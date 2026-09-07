using JaySharp.Shared.Evaluations.Lists;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class ArrayTests
{
    [JayTest]
    public static void AreSame()
    {
        int[] underTest = new int[] { 1, 2, 3 };
        int[] toCompare = new int[] { 1, 2, 3 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_Same_Size()
    {
        int[] underTest = new int[] { 1, 2, 3 };
        int[] toCompare = new int[] { 1, 2, 6 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller()
    {
        int[] underTest = new int[] { 1, 2, 3 };
        int[] toCompare = new int[] { 1 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller_2()
    {
        int[] underTest = new int[] { 1, 2, 3, 5, 6 };
        int[] toCompare = new int[] { 1, 2, 3 };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_larger()
    {
        int[] underTest = new int[] { 1, 2, 3 };
        int[] toCompare = new int[] { 1, 2, 3, 5, 6 };
        underTest.Oughta().Be(toCompare);
    }

    [JayTest]
    public static void AreNotSame_differ_by_size_and_content()
    {
        int[] underTest = new int[] { 4, 5, 6 };
        int[] toCompare = new int[] { 1, 2, 3, 9 };
        underTest.Oughta().Be(toCompare);
    }
}