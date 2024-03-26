namespace Api.Common.Models.EnvironmentVariables;

public class EnvironmentVariableConfigurationSource : IConfigurationSource
{
    private readonly string _prefix;

    public EnvironmentVariableConfigurationSource(string prefix)
    {
        _prefix = prefix;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new EnvironmentVariableConfigurationProvider(_prefix);
    }
}
