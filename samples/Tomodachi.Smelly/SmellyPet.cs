using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;

namespace Tomodachi.Smelly;

public class SmellyPet
{
    public string Name { get; set; }
    public int Hunger { get; set; } = 50;
    public int Happiness { get; set; } = 50;

    public SmellyPet(string name)
    {
        var validName = name != null;
        validName.Must().Be(true);
        Name = name!;
    }

    public void Feed(int portion = 20)
    {
        var isPositive = portion > 0;
        isPositive.Must().Be(true);

        var isHungry = Hunger > 0;
        isHungry.Oughta().Be(true);

        Hunger = Math.Max(0, Hunger - portion);
    }
}
