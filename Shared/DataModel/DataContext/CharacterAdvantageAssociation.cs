using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class CharacterAdvantageAssociation
    {
        public long CharacterFk { get; set; }
        public long AdvantageFk { get; set; }

        public virtual Advantage AdvantageFkNavigation { get; set; }
        public virtual Character CharacterFkNavigation { get; set; }
    }
}
