using JaySharp.Linq;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;
using Is = JaySharp.TestSuite.TestAttributes.Is;

namespace JaySharp.Tests;

public class DummyPet
{
    public string Name { get; set; } = "Tama";
    public int Hunger { get; set; } = 0;
    public int Happiness { get; set; } = 80;
}

[JayTestSuite(On = Is.On, Description = "Validates LINQ query syntax assertions (from / in / where / join / equals / select).")]
public static class LinqAssertionTests
{
    [JayTest(On = Is.On, Description = "LINQ where clause evaluates primitive value assertion.")]
    public static void LinqWhere_Primitive_ExecutesCleanly()
    {
        int hunger = 0;
        (from h in hunger.Must()
         where h == 0
         select h).Be(0);
    }

    [JayTest(On = Is.On, Description = "LINQ where clause evaluates soft warning for Oughta.")]
    public static void LinqWhere_SoftWarning_ExecutesCleanly()
    {
        int happiness = 75;
        _ = from h in happiness.Oughta()
            where h > 50
            select h;
    }

    [JayTest(On = Is.On, Description = "LINQ join ... on actual equals expected evaluates equality.")]
    public static void LinqJoinEquals_ExecutesCleanly()
    {
        int actualScore = 100;
        int expectedScore = 100;

        _ = from actual in actualScore.Must()
            join expected in expectedScore on actual equals expected
            select actual;
    }

    [JayTest(On = Is.On, Description = "LINQ multi-where clause pipeline validates object graph properties.")]
    public static void LinqMultiWhere_ObjectGraph_ExecutesCleanly()
    {
        DummyPet pet = new DummyPet { Name = "Tama", Hunger = 0, Happiness = 90 };

        _ = from p in pet.Must()
            where p.Hunger == 0
            where p.Happiness > 50
            where p.Name.StartsWith("Tama")
            select p;
    }
}
