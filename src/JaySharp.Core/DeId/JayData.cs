using System.Reflection;
using System.Text;
using System.Text.Json;

namespace JaySharp.DeId;

/// <summary>
/// Utility for generating labeled test data scenarios and exporting them to JSON or SQL seed scripts.
/// </summary>
public static class JayData
{
    /// <summary>
    /// Creates a labeled test scenario instance bound to a specific record ID and persona label.
    /// </summary>
    /// <typeparam name="T">The target entity type.</typeparam>
    /// <param name="id">The primary key ID to preserve for SQL relational joins.</param>
    /// <param name="label">The memorable persona label (e.g. "George Washington [Subscriber with Dependents]").</param>
    /// <param name="configure">Optional delegate to set custom domain property values.</param>
    /// <returns>A populated scenario instance of <typeparamref name="T"/>.</returns>
    public static T CreateScenario<T>(int id, string label, Action<T>? configure = null) where T : new()
    {
        T instance = new T();
        Type type = typeof(T);
        string cleanLabelSlug = label.ToLowerInvariant()
            .Replace(" ", "_")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("[", "")
            .Replace("]", "");

        if (cleanLabelSlug.Length > 20) cleanLabelSlug = cleanLabelSlug[..20];

        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in properties)
        {
            if (!prop.CanWrite) continue;

            string propName = prop.Name.ToLowerInvariant();

            // Set Primary Key ID
            if (prop.PropertyType == typeof(int) && (propName == "id" || propName.EndsWith("id")))
            {
                prop.SetValue(instance, id);
            }
            // Set Memorable Label / Persona Name
            else if (prop.PropertyType == typeof(string))
            {
                if (propName.Contains("name"))
                {
                    prop.SetValue(instance, label);
                }
                else if (propName.Contains("email"))
                {
                    prop.SetValue(instance, $"{cleanLabelSlug}_{id}@scenario.local");
                }
                else if (propName.Contains("ssn") || propName.Contains("socialsecurity"))
                {
                    prop.SetValue(instance, $"***-**-{id:D4}"[^9..]);
                }
                else if (propName.Contains("notes") || propName.Contains("description"))
                {
                    prop.SetValue(instance, $"Scenario Fixture: {label} (ID: {id})");
                }
            }
        }

        configure?.Invoke(instance);
        return instance;
    }

    /// <summary>
    /// Serializes scenario records to an indented JSON string representation.
    /// </summary>
    /// <param name="items">The scenario objects to serialize.</param>
    /// <returns>Formatted JSON string.</returns>
    public static string ExportJson(params object[] items)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        if (items.Length == 1)
        {
            return JsonSerializer.Serialize(items[0], options);
        }
        return JsonSerializer.Serialize(items, options);
    }

    /// <summary>
    /// Writes scenario records to a JSON file.
    /// </summary>
    /// <param name="filePath">Target JSON file output path.</param>
    /// <param name="items">The scenario objects to serialize.</param>
    public static void ExportJsonFile(string filePath, params object[] items)
    {
        string json = ExportJson(items);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Generates SQL INSERT statements for local database seeding (SQLite / PostgreSQL / SQL Server).
    /// </summary>
    /// <param name="tableName">The target database table name.</param>
    /// <param name="items">The scenario records to convert into SQL INSERT statements.</param>
    /// <returns>Formatted SQL script.</returns>
    public static string ExportSqlInsert(string tableName, params object[] items)
    {
        if (items.Length == 0) return string.Empty;

        StringBuilder sb = new StringBuilder();

        foreach (object item in items)
        {
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .ToArray();

            string columnNames = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => FormatSqlValue(p.GetValue(item))));

            sb.AppendLine($"INSERT INTO {tableName} ({columnNames}) VALUES ({values});");
        }

        return sb.ToString().TrimEnd();
    }

    private static string FormatSqlValue(object? val)
    {
        if (val == null) return "NULL";
        if (val is bool b) return b ? "1" : "0";
        if (val is int || val is long || val is double || val is decimal || val is float) return val.ToString()!;
        if (val is DateTime dt) return $"'{dt:yyyy-MM-dd HH:mm:ss}'";

        string str = val.ToString() ?? string.Empty;
        return $"'{str.Replace("'", "''")}'";
    }
}
