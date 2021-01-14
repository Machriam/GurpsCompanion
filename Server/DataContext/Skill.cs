using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Server.DataContext
{
    public partial class Skill
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Value { get; set; }
    }
}
