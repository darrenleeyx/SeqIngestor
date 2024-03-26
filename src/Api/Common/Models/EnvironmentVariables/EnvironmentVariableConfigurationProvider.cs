using System.Collections;

namespace Api.Common.Models.EnvironmentVariables;

public class EnvironmentVariableConfigurationProvider : ConfigurationProvider
{
    private readonly string _prefix;

    public EnvironmentVariableConfigurationProvider(string prefix)
    {
        _prefix = prefix;
    }

    public override void Load()
    {
        Data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (DictionaryEntry envVariable in Environment.GetEnvironmentVariables())
        {
            string? key = envVariable.Key.ToString();
            string value = envVariable.Value?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(key) &&
                !string.IsNullOrEmpty(value) &&
                key.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
            {
                key = key.Substring(_prefix.Length);
                key = key.Replace('_', ':');

                Data[key] = value;
            }
        }
    }
}
