namespace JaySharp.DeId;

/// <summary>
/// Specifies the masking and scrambling mode for PHI/PII de-identification.
/// </summary>
public enum MaskMode
{
    /// <summary>Auto-detects sensitivity based on property name.</summary>
    Auto,

    /// <summary>Replaces full name with a realistic synthetic name (e.g., "Alex Smith").</summary>
    Name,

    /// <summary>Masks Social Security Number (e.g., "***-**-6789").</summary>
    Ssn,

    /// <summary>Replaces email with a synthetic email (e.g., "anon_4910@deid.internal").</summary>
    Email,

    /// <summary>Replaces phone number with a safe synthetic format (e.g., "555-019-9821").</summary>
    Phone,

    /// <summary>Jitters DateTime values by a fixed or random day offset while preserving age/interval.</summary>
    DateShift,

    /// <summary>Generates a deterministic SHA256 pseudonymized hash.</summary>
    Hash,

    /// <summary>Replaces value with "[REDACTED]".</summary>
    Redact
}

/// <summary>
/// Marks a property or field as containing sensitive PHI/PII data for automatic de-identification and scrambling.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class JaySensitiveAttribute : Attribute
{
    /// <summary>Gets the masking mode applied to the target element.</summary>
    public MaskMode Mode { get; }

    /// <summary>Gets or sets the day offset applied when using <see cref="MaskMode.DateShift"/>.</summary>
    public int ShiftDays { get; set; } = -14;

    /// <summary>
    /// Initializes a new instance of the <see cref="JaySensitiveAttribute"/> class.
    /// </summary>
    /// <param name="mode">The masking and scrambling strategy.</param>
    public JaySensitiveAttribute(MaskMode mode = MaskMode.Auto)
    {
        Mode = mode;
    }
}
