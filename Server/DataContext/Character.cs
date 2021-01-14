using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Server.DataContext
{
    public partial class Character
    {
        public long Id { get; set; }
        public long IsPlayer { get; set; }
        public string Name { get; set; }
        public long Strength { get; set; }
        public long Dexterity { get; set; }
        public long Intelligence { get; set; }
        public long Health { get; set; }
        public long WillMod { get; set; }
        public long PerceptionMod { get; set; }
        public long HitPointsMod { get; set; }
        public long BasicSpeedMod { get; set; }
        public long BasicMoveMod { get; set; }
        public long RadexFavor { get; set; }
        public long VagrexFavor { get; set; }
    }
}
