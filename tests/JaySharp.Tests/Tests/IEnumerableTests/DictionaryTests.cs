using JaySharp.Shared.Evaluations.Dictionaries;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class DictionaryTests
{
    [JayTest]
    public static void AreSame()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_Same_Size()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 6, 3 } };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 } };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_smaller_2()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 5, 5 }, { 6, 6 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        underTest.Oughta().Be(toCompare);
    }
    [JayTest]
    public static void AreNotSame_comparison_larger()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 5, 5 }, { 6, 6 } };
        underTest.Oughta().Be(toCompare);
    }

    [JayTest]
    public static void AreNotSame_differ_by_size_and_content()
    {
        Dictionary<int, int> underTest = new Dictionary<int, int> { { 4, 4 }, { 5, 5 }, { 6, 6 } };
        Dictionary<int, int> toCompare = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 9, 9 } };
        underTest.Oughta().Be(toCompare);
    }
}