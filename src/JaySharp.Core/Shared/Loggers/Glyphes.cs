namespace JaySharp.Shared.Loggers;

/// <summary>
/// Centralized log framing tag constants and formatting helpers for JaySharp.
/// </summary>
public static class Glyphes
{
    /// <summary>Opening tag for passed assertion and success logs ("¡¡").</summary>
    public const string PassOpen = "¡¡";
    /// <summary>Closing tag for passed assertion and success logs ("!!").</summary>
    public const string PassClose = "!!";

    /// <summary>Opening tag for failed assertion and exception logs ("¿¿").</summary>
    public const string FailOpen = "¿¿";
    /// <summary>Closing tag for failed assertion and exception logs ("??").</summary>
    public const string FailClose = "??";

    /// <summary>Opening tag for soft warning logs ("??").</summary>
    public const string WarningOpen = "??";
    /// <summary>Closing tag for soft warning logs ("??").</summary>
    public const string WarningClose = "??";

    /// <summary>Opening tag for informational context logs ("~~").</summary>
    public const string InfoOpen = "~~";
    /// <summary>Closing tag for informational context logs ("~~").</summary>
    public const string InfoClose = "~~";

    /// <summary>Opening tag for section header logs ("||").</summary>
    public const string SectionOpen = "||";
    /// <summary>Closing tag for section header logs ("||").</summary>
    public const string SectionClose = "||";

    /// <summary>Formats a message with success pass glyph tags ("¡¡ message !!").</summary>
    public static string Pass(string message) => $"{PassOpen} {message} {PassClose}";

    /// <summary>Formats a message with failure glyph tags ("¿¿ message ??").</summary>
    public static string Fail(string message) => $"{FailOpen} {message} {FailClose}";

    /// <summary>Formats a message with warning glyph tags ("?? message ??").</summary>
    public static string Warning(string message) => $"{WarningOpen} {message} {WarningClose}";

    /// <summary>Formats a message with informational glyph tags ("~~ message ~~").</summary>
    public static string Info(string message) => $"{InfoOpen} {message} {InfoClose}";

    /// <summary>Formats a message with section header glyph tags ("|| message ||").</summary>
    public static string Section(string message) => $"{SectionOpen} {message} {SectionClose}";
}
