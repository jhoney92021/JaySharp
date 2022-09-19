using JaySharp.Shared.Evaluations.Characters;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On)]
public static class CharacterTests
{
    [JayTest("CompareCharacters")]
    public static void CompareCharacters()
    {
        var underTest = 'p';
        underTest.Oughta().Be('p');
    }
    [JayTest("CompareCharacters_Fail")]
    public static void CompareCharacters_Fail()
    {
        var underTest = 'p';
        underTest.Oughta().Be('Q');
    }
    [JayTest("CompareCharacters_Must_Be")]
    public static void CompareCharacters_Must_Be()
    {
        var underTest = '1';
        underTest.Must().Be('1');
    }
    [JayTest("CompareCharacters_Must_Be_Fail", On = Is.Off)]
    public static void CompareCharacters_Must_Be_Fail()
    {
        var underTest = 'q';
        
        underTest.Must().Be('P');
    }
}