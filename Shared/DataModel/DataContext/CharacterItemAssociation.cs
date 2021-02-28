using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class CharacterItemAssociation
    {
        public long CharacterFk { get; set; }
        public long ItemFk { get; set; }
        public long Id { get; set; }

        public virtual Character CharacterFkNavigation { get; set; }
        public virtual Item ItemFkNavigation { get; set; }
    }
}
