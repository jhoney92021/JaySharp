
namespace JaySharp.TestRunner;
[System.AttributeUsage(System.AttributeTargets.Method)] 
public class JayTest : System.Attribute
{
    public string Name {get;set;} = "unset";

    public JayTest(string name)
    {
        Name = name;
    }
}