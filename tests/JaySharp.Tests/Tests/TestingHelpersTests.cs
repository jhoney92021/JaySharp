using System.Net;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;
using JaySharp.Testing;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

/// <summary>
/// Verifies JayHttpClient and FakeRepository testing helpers.
/// </summary>
[JayTestSuite(On = Is.On)]
public static class TestingHelpersTests
{
    [JayTest(Description = "JayHttpClient returns canned JSON response without network call.", On = Is.On)]
    public static void JayHttpClient_ReturnsCannedJsonResponse()
    {
        string json = "{\"name\":\"Tama\",\"hunger\":30}";
        HttpClient client = JayHttpClient.Create(json, HttpStatusCode.OK);

        string response = client.GetStringAsync("https://fake-api.internal/pet").GetAwaiter().GetResult();
        response.Must().Be(json);
    }

    [JayTest(Description = "FakeRepository provides in-memory CRUD operations.", On = Is.On)]
    public static void FakeRepository_PerformsCrudOperations()
    {
        FakeRepository<int, string> repo = new();
        repo.Add(1, "Tama");
        repo.Add(2, "SmellyTama");

        repo.Count.Must().Be(2);

        string? pet = repo.Get(1);
        (pet != null).Must().Be(true);
        pet!.Must().Be("Tama");

        bool deleted = repo.Delete(1);
        deleted.Must().Be(true);
        repo.Count.Must().Be(1);
    }
}
