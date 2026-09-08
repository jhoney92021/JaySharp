using JaySharp.DeId;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.Evaluations.Type;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;
using Is = JaySharp.TestSuite.TestAttributes.Is;

namespace JaySharp.Tests;

public class PatientRecordDto
{
    public int PatientId { get; set; } = 101;

    [JaySensitive(MaskMode.Name)]
    public string FullName { get; set; } = "Jane Doe";

    [JaySensitive(MaskMode.Ssn)]
    public string SocialSecurityNumber { get; set; } = "123-45-6789";

    [JaySensitive(MaskMode.Email)]
    public string EmailAddress { get; set; } = "jane.doe@hospital.org";

    [JaySensitive(MaskMode.Phone)]
    public string ContactPhone { get; set; } = "415-555-1234";

    [JaySensitive(MaskMode.DateShift, ShiftDays = -10)]
    public DateTime BirthDate { get; set; } = new DateTime(1990, 5, 20);

    [JaySensitive(MaskMode.Redact)]
    public string MedicalNotes { get; set; } = "Confidential diagnosis notes";

    [JaySensitive(MaskMode.Hash)]
    public string AccountNumber { get; set; } = "ACC-99201";
}

public class UnannotatedPatientDto
{
    public string PatientName { get; set; } = "John Smith";
    public string Ssn { get; set; } = "987-65-4321";
    public string Email { get; set; } = "john@hospital.org";
}

[JayTestSuite(On = Is.On, Description = "Validates JayDeId PHI/PII de-identification and scrambling engine.")]
public static class DeIdTests
{
    [JayTest(On = Is.On, Description = "Explicit JaySensitive attributes scramble PHI data correctly.")]
    public static void ExplicitAttributes_ScramblePHIData()
    {
        PatientRecordDto raw = new PatientRecordDto();
        PatientRecordDto sanitized = JayDeId.Scramble(raw);

        // PatientId is preserved
        sanitized.PatientId.Must().Be(101);

        // Name is replaced with a synthetic name
        (sanitized.FullName != "Jane Doe").Must().Be(true);

        // SSN is masked
        sanitized.SocialSecurityNumber.Must().Be("***-**-6789");

        // Email is anonymized
        sanitized.EmailAddress.Must().Contain("@deid.internal");

        // Phone is safe 555 format
        sanitized.ContactPhone.Must().StartWith("555-019-");

        // BirthDate is date shifted by -10 days
        sanitized.BirthDate.Must().Be(new DateTime(1990, 5, 10));

        // MedicalNotes are redacted
        sanitized.MedicalNotes.Must().Be("[REDACTED]");

        // AccountNumber is hashed
        (sanitized.AccountNumber != "ACC-99201").Must().Be(true);
    }

    [JayTest(On = Is.On, Description = "Auto-detect mode scrambles common PHI property names when unannotated.")]
    public static void AutoDetect_ScramblesCommonPropertyNames()
    {
        UnannotatedPatientDto raw = new UnannotatedPatientDto();
        UnannotatedPatientDto sanitized = JayDeId.Scramble(raw, autoDetect: true);

        (sanitized.PatientName != "John Smith").Must().Be(true);
        sanitized.Ssn.Must().Be("***-**-4321");
        sanitized.Email.Must().Contain("@deid.internal");
    }

    [JayTest(On = Is.On, Description = "JayData creates labeled test scenario objects with preserved PK IDs.")]
    public static void JayData_CreateScenario_GeneratesLabeledRecord()
    {
        PatientRecordDto scenario1 = JayData.CreateScenario<PatientRecordDto>(
            id: 101,
            label: "George Washington [Subscriber with Dependents]",
            configure: p => p.MedicalNotes = "Scenario Notes"
        );

        scenario1.PatientId.Must().Be(101);
        scenario1.FullName.Must().Be("George Washington [Subscriber with Dependents]");
        scenario1.EmailAddress.Must().Contain("101@scenario.local");
        scenario1.MedicalNotes.Must().Be("Scenario Notes");
    }

    [JayTest(On = Is.On, Description = "JayData exports scenarios to JSON and SQL INSERT scripts.")]
    public static void JayData_ExportsJsonAndSqlInsert()
    {
        PatientRecordDto scenario1 = JayData.CreateScenario<PatientRecordDto>(
            id: 101,
            label: "George Washington"
        );

        string json = JayData.ExportJson(scenario1);
        json.Must().Contain("\"PatientId\": 101");
        json.Must().Contain("\"FullName\": \"George Washington\"");

        string sql = JayData.ExportSqlInsert("Patients", scenario1);
        sql.Must().StartWith("INSERT INTO Patients (");
        sql.Must().Contain("VALUES (101, 'George Washington'");
    }
}
