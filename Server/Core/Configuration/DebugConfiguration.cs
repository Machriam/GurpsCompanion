namespace GurpsCompanion.Server.Core
{
    public class DebugConfiguration : IEnvironmentConfiguration
    {
        private readonly ConfigurationOptions _options;

        public DebugConfiguration(ConfigurationOptions options)
        {
            _options = options;
        }

        public string DatabaseConnection() => "Data Source=" + _options.DatabaseConnection;
        public string GameMasterPasswordHash() => _options.GameMasterPasswordHash;
    }
}
