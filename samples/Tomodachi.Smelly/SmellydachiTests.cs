using JaySharp.FeatureFlagging.Attributes;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace Tomodachi.Smelly;

[JayTestSuite(On = Is.On)]
[JayFeature("SmellydachiCore")]
public static class SmellydachiTests
{
    [JayTest(Description = "Assertion-Less Test: Executes code but makes no assertions!", On = Is.On)]
    public static void FeedingPet_DoesNothingImportant()
    {
        SmellyPet pet = new SmellyPet("SmellyTama");
        pet.Feed();
    }

    [JayTest(Description = "Tautological / Trivial Test: Always passes trivially!", On = Is.On)]
    public static void TrivialCheck_AlwaysPasses()
    {
        "SmellyTama".Must().Be("SmellyTama");
        10.Oughta().Be(10);
    }

    [JayTest(Description = "Disabled Test Cruft: Test permanently turned off", On = Is.Off)]
    public static void AbandonedTest_MarkedOff()
    {
        SmellyPet pet = new SmellyPet("SmellyTama");
        pet.Hunger.Oughta().Be(0);
    }
}
