using System.IO;

namespace GurpsCompanion.Server.Core
{
    public interface IEnvironmentConfiguration
    {
        string DatabaseConnection { get; }
        string GameMasterPasswordHash { get; }
    }

    public class ReleaseConfiguration : IEnvironmentConfiguration
    {
        private readonly ConfigurationOptions _options;

        public ReleaseConfiguration(ConfigurationOptions options)
        {
            _options = options;
        }

        public string DatabaseConnection => "Data Source=" + Directory.GetCurrentDirectory() + "\\" + _options.DatabaseConnection;
        public string GameMasterPasswordHash => _options.GameMasterPasswordHash;
    }
}
