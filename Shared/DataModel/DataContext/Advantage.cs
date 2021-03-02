using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Advantage
    {
        public Advantage()
        {
            CharacterAdvantageAssociations = new HashSet<CharacterAdvantageAssociation>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Cost { get; set; }
        public long Skillable { get; set; }

        public virtual ICollection<CharacterAdvantageAssociation> CharacterAdvantageAssociations { get; set; }
    }
}
