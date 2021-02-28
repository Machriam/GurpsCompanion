using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Item
    {
        public Item()
        {
            CharacterItemAssociations = new HashSet<CharacterItemAssociation>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public long Id { get; set; }
        public byte[] Image { get; set; }
        public long Weight { get; set; }

        public virtual ICollection<CharacterItemAssociation> CharacterItemAssociations { get; set; }
    }
}
