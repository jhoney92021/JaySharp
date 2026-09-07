using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;

namespace Tomodachi;

public class Pet
{
    public string Name { get; set; }
    public int Hunger { get; set; } = 50;
    public int Happiness { get; set; } = 50;
    public int Energy { get; set; } = 50;

    public Pet(string name)
    {
        (name != null).Must().Be(true);
        Name = name!;
    }

    public void Feed(int portion = 20)
    {
        // Defensive Guard (Must): Reject invalid portion sizes
        (portion > 0).Must().Be(true);

        // Defensive Soft Guard (Oughta): Warn if feeding an already full pet
        (Hunger > 0).Oughta().Be(true);

        Hunger = Math.Max(0, Hunger - portion);
        Happiness = Math.Min(100, Happiness + 5);
    }

    public void Play()
    {
        // Defensive Soft Guard (Oughta): Warn if pet is playing while exhausted
        (Energy >= 10).Oughta().Be(true);

        Happiness = Math.Min(100, Happiness + 15);
        Energy = Math.Max(0, Energy - 10);
        Hunger = Math.Min(100, Hunger + 10);
    }

    public void Sleep()
    {
        Energy = Math.Min(100, Energy + 30);
    }
}
