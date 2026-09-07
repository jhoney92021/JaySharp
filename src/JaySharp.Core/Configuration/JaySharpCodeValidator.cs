using System.Text.RegularExpressions;

namespace JaySharp.Configuration;

/// <summary>
/// Represents a single code style or test smell violation detected by <see cref="JaySharpCodeValidator"/>.
/// </summary>
/// <param name="FilePath">The full path to the source file containing the violation.</param>
/// <param name="LineNumber">The 1-indexed line number where the violation occurred.</param>
/// <param name="LineContent">The source code line content.</param>
/// <param name="RuleName">The identifier of the rule that was triggered.</param>
public record CodeViolation(string FilePath, int LineNumber, string LineContent, string RuleName);

/// <summary>
/// Static analyzer that validates C# code against configured style policies and detects test code smells (junk tests).
/// </summary>
public static class JaySharpCodeValidator
{
    private static readonly Regex VarDeclarationRegex = new(@"\bvar\s+[a-zA-Z_][a-zA-Z0-9_]*\s*=", RegexOptions.Compiled);

    /// <summary>
    /// Validates whether a source code string satisfies the explicit type declaration policy.
    /// </summary>
    /// <param name="sourceCode">The source code to inspect.</param>
    /// <param name="config">Optional configuration overrides; defaults to <see cref="JaySharpConfig.Current"/>.</param>
    /// <returns><c>true</c> if compliant; otherwise, <c>false</c>.</returns>
    public static bool ValidateExplicitVar(string sourceCode, JaySharpConfig? config = null)
    {
        config ??= JaySharpConfig.Current;

        if (!config.EnforceExplicitVar)
        {
            return true;
        }

        bool containsVarDeclaration = VarDeclarationRegex.IsMatch(sourceCode);
        return !containsVarDeclaration;
    }

    /// <summary>
    /// Enforces the explicit type declaration policy on source code, throwing an exception if violated.
    /// </summary>
    /// <param name="sourceCode">The source code to inspect.</param>
    /// <param name="config">Optional configuration overrides.</param>
    /// <exception cref="CodeStyleViolationException">Thrown when an implicit 'var' declaration is detected.</exception>
    public static void EnforceExplicitVar(string sourceCode, JaySharpConfig? config = null)
    {
        config ??= JaySharpConfig.Current;

        if (!ValidateExplicitVar(sourceCode, config))
        {
            throw new CodeStyleViolationException("Code policy violation: implicit 'var' declaration detected in source code.");
        }
    }

    /// <summary>
    /// Recursively scans a directory for C# files and checks them against all code quality rules and junk test detectors.
    /// </summary>
    /// <param name="directoryPath">The directory path to analyze.</param>
    /// <param name="config">Optional configuration settings.</param>
    /// <returns>A list of detected <see cref="CodeViolation"/> records.</returns>
    public static List<CodeViolation> ValidateDirectory(string directoryPath, JaySharpConfig? config = null)
    {
        config ??= JaySharpConfig.Current;
        List<CodeViolation> violations = new();

        if (!Directory.Exists(directoryPath))
        {
            return violations;
        }

        string[] csFiles = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories)
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}") &&
                           !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}"))
            .ToArray();

        foreach (string filePath in csFiles)
        {
            violations.AddRange(ValidateFile(filePath, config));
        }

        return violations;
    }

    public static List<CodeViolation> ValidateFile(string filePath, JaySharpConfig? config = null)
    {
        config ??= JaySharpConfig.Current;
        List<CodeViolation> violations = new();

        if (!File.Exists(filePath))
        {
            return violations;
        }

        string[] lines = File.ReadAllLines(filePath);
        for (int lineNumber = 1; lineNumber <= lines.Length; lineNumber++)
        {
            string line = lines[lineNumber - 1];
            string trimmed = line.Trim();

            if (trimmed.StartsWith("//", StringComparison.Ordinal) ||
                trimmed.StartsWith("/*", StringComparison.Ordinal) ||
                trimmed.StartsWith('*'))
            {
                continue;
            }

            string lineWithoutStrings = Regex.Replace(line, @"""[^""]*""", "");

            if (config.EnforceExplicitVar && VarDeclarationRegex.IsMatch(lineWithoutStrings))
            {
                violations.Add(new CodeViolation(filePath, lineNumber, line.Trim(), "EnforceExplicitVar"));
            }

            if (trimmed.Contains("[JayTest") && !trimmed.Contains("[JayTestSuite") && trimmed.Contains("Is.Off"))
            {
                violations.Add(new CodeViolation(filePath, lineNumber, line.Trim(), "DisabledTestCruft"));
            }

            // Tautological literal assertion check (e.g., "A".Must().Be("A") or 10.Oughta().Be(10))
            Match tautologyMatch = Regex.Match(line, @"("".+?""|\b\d+\b)\.(Must|Oughta)\(\)\.Be\(\1\)");
            if (tautologyMatch.Success)
            {
                violations.Add(new CodeViolation(filePath, lineNumber, line.Trim(), "TautologicalAssertion"));
            }
        }

        // Method-level inspection for assertion-less test methods
        violations.AddRange(DetectAssertionLessTests(filePath, lines));

        return violations;
    }

    private static List<CodeViolation> DetectAssertionLessTests(string filePath, string[] lines)
    {
        List<CodeViolation> violations = new();
        bool insideTest = false;
        int testStartLine = 0;
        string testHeader = string.Empty;
        bool hasAssertion = false;

        for (int index = 0; index < lines.Length; index++)
        {
            string line = lines[index].Trim();

            if (line.StartsWith("[JayTest") && !line.StartsWith("[JayTestSuite"))
            {
                if (insideTest && !hasAssertion)
                {
                    violations.Add(new CodeViolation(filePath, testStartLine, testHeader, "AssertionLessTest"));
                }

                insideTest = true;
                testStartLine = index + 1;
                testHeader = line;
                hasAssertion = false;
                continue;
            }

            if (insideTest)
            {
                if (line.Contains(".Must()") || line.Contains(".Oughta()") || line.Contains(".IsTrue()") || line.Contains(".IsOn()") || line.Contains(".IsOff()"))
                {
                    hasAssertion = true;
                }

                if (line.StartsWith("public static void") || line.StartsWith("public void") || line.StartsWith("private void"))
                {
                    testHeader = line;
                }
            }
        }

        if (insideTest && !hasAssertion)
        {
            violations.Add(new CodeViolation(filePath, testStartLine, testHeader, "AssertionLessTest"));
        }

        return violations;
    }
}
