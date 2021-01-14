namespace GurpsCompanion.Server.Core
{
    public interface IEnvironmentConfiguration
    {
        string DatabaseConnection();
    }

    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        private readonly ConfigurationOptions _options;

        public EnvironmentConfiguration(ConfigurationOptions options)
        {
            _options = options;
        }

        public string DatabaseConnection() => _options.DatabaseConnection;
    }
}