using JaySharp.FeatureFlagging.Attributes;
using JaySharp.TestSuite.TestAttributes;

namespace Tomodachi;

[JayFeature("MiniGames", On = Is.On)]
public static class MiniGames
{
    public static int PlayFetch(Pet pet)
    {
        pet.Play();
        return 100; // Earned score
    }
}
