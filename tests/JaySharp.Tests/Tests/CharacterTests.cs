using JaySharp.Shared.Evaluations.Characters;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class CharacterTests
{
    [JayTest(Name = "CompareCharacters")]
    public static void CompareCharacters()
    {
        char underTest = 'p';
        underTest.Oughta().Be('p');
    }
    [JayTest(Name = "CompareCharacters_Fail")]
    public static void CompareCharacters_Fail()
    {
        char underTest = 'p';
        underTest.Oughta().Be('Q');
    }
    [JayTest(Name = "CompareCharacters_Must_Be")]
    public static void CompareCharacters_Must_Be()
    {
        char underTest = '1';
        underTest.Must().Be('1');
    }
    [JayTest(Name = "CompareCharacters_Must_Be_Fail", On = Is.Off)]
    public static void CompareCharacters_Must_Be_Fail()
    {
        char underTest = 'q';

        underTest.Must().Be('P');
    }
}