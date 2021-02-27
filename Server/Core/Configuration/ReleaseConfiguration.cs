namespace GurpsCompanion.Server.Core
{
    public interface IEnvironmentConfiguration
    {
        string DatabaseConnection();
        string GameMasterPasswordHash();
    }

    public class ReleaseConfiguration : IEnvironmentConfiguration
    {
        private readonly ConfigurationOptions _options;

        public ReleaseConfiguration(ConfigurationOptions options)
        {
            _options = options;
        }

        public string DatabaseConnection() => "Data Source=" + _options.DatabaseConnection;
        public string GameMasterPasswordHash() => _options.GameMasterPasswordHash;
    }
}
