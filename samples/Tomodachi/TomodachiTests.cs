using JaySharp.FeatureFlagging.Attributes;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace Tomodachi;

[JayTestSuite(On = Is.On)]
[JayFeature("TomodachiCore")]
public static class TomodachiTests
{
    [JayTest(On = Is.On)]
    public static void FeedingPet_ReducesHunger()
    {
        Pet pet = new Pet("Tama");
        int initialHunger = pet.Hunger;

        pet.Feed();

        (pet.Hunger < initialHunger).Oughta().Be(true);
        pet.Hunger.Oughta().Be(30);
    }

    [JayTest(On = Is.On)]
    public static void PlayingWithPet_IncreasesHappiness()
    {
        Pet pet = new Pet("Tama");
        int initialHappiness = pet.Happiness;

        pet.Play();

        (pet.Happiness > initialHappiness).Must().Be(true);
        pet.Happiness.Oughta().Be(65);
    }
}
