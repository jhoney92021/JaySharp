using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace JaySharp.DeId;

/// <summary>
/// Reflection-based PHI/PII de-identification engine for sanitizing sensitive test data and API payloads.
/// </summary>
public static class JayDeId
{
    private static readonly string[] FirstNames = new[] { "Alex", "Jordan", "Morgan", "Taylor", "Casey", "Riley", "Avery", "Dakota", "Reese", "Quinn" };
    private static readonly string[] LastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };

    /// <summary>
    /// De-identifies and scrambles sensitive PHI/PII fields on an object or data record.
    /// </summary>
    /// <typeparam name="T">The target object type.</typeparam>
    /// <param name="source">The source object to sanitize.</param>
    /// <param name="autoDetect">If true, automatically infers masking modes for common PHI property names when unannotated.</param>
    /// <returns>A new de-identified instance of <typeparamref name="T"/> with scrambled sensitive data.</returns>
    public static T Scramble<T>(T source, bool autoDetect = true)
    {
        if (source == null) return source;

        Type type = typeof(T);
        T target = (T)Activator.CreateInstance(type)!;

        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in properties)
        {
            if (!prop.CanRead || !prop.CanWrite) continue;

            object? rawValue = prop.GetValue(source);
            if (rawValue == null)
            {
                prop.SetValue(target, null);
                continue;
            }

            JaySensitiveAttribute? attr = prop.GetCustomAttribute<JaySensitiveAttribute>();
            MaskMode mode = attr?.Mode ?? MaskMode.Auto;
            int shiftDays = attr?.ShiftDays ?? -14;

            if (mode == MaskMode.Auto && autoDetect)
            {
                mode = InferMaskMode(prop.Name);
            }

            object? sanitizedValue = ApplyMask(rawValue, mode, prop.Name, shiftDays);
            prop.SetValue(target, sanitizedValue);
        }

        return target;
    }

    private static MaskMode InferMaskMode(string propertyName)
    {
        string name = propertyName.ToLowerInvariant();
        if (name.Contains("ssn") || name.Contains("socialsecurity")) return MaskMode.Ssn;
        if (name.Contains("email")) return MaskMode.Email;
        if (name.Contains("phone") || name.Contains("mobile")) return MaskMode.Phone;
        if (name.Contains("name") || name.Contains("patientname")) return MaskMode.Name;
        if (name.Contains("birth") || name.Contains("dob")) return MaskMode.DateShift;

        return MaskMode.Auto;
    }

    private static object? ApplyMask(object rawValue, MaskMode mode, string propName, int shiftDays)
    {
        if (mode == MaskMode.Auto) return rawValue;

        if (rawValue is DateTime dt && mode == MaskMode.DateShift)
        {
            return dt.AddDays(shiftDays);
        }

        string valStr = rawValue.ToString() ?? string.Empty;

        return mode switch
        {
            MaskMode.Name => GenerateFakeName(valStr + propName),
            MaskMode.Ssn => MaskSsn(valStr),
            MaskMode.Email => MaskEmail(valStr + propName),
            MaskMode.Phone => MaskPhone(valStr + propName),
            MaskMode.Hash => ComputeSha256(valStr),
            MaskMode.Redact => "[REDACTED]",
            MaskMode.DateShift when rawValue is DateTime date => date.AddDays(shiftDays),
            _ => rawValue
        };
    }

    private static string GenerateFakeName(string seedInput)
    {
        int hash = Math.Abs(seedInput.GetHashCode());
        string firstName = FirstNames[hash % FirstNames.Length];
        string lastName = LastNames[(hash / 10) % LastNames.Length];
        return $"{firstName} {lastName}";
    }

    private static string MaskSsn(string rawSsn)
    {
        if (rawSsn.Length >= 4)
        {
            string last4 = rawSsn[^4..];
            return $"***-**-{last4}";
        }
        return "***-**-6789";
    }

    private static string MaskEmail(string seedInput)
    {
        int hash = Math.Abs(seedInput.GetHashCode()) % 9000 + 1000;
        return $"anon_{hash}@deid.internal";
    }

    private static string MaskPhone(string seedInput)
    {
        int hash = Math.Abs(seedInput.GetHashCode()) % 9000 + 1000;
        return $"555-019-{hash}";
    }

    private static string ComputeSha256(string rawInput)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawInput));
        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString()[..12];
    }
}
