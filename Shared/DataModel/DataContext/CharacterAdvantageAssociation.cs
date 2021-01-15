using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class CharacterAdvantageAssociation
    {
        public long CharacterFk { get; set; }
        public long AdvantageVk { get; set; }

        public virtual Character CharacterFkNavigation { get; set; }
    }
}
