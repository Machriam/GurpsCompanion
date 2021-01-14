using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GurpsCompanion.Server.Core
{
    public class ConfigurationOptions
    {
        public const string Configuration = "Configuration";
        public string DatabaseConnection { get; set; }
    }
}
