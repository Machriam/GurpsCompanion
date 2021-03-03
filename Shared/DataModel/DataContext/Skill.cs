using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Skill
    {
        public Skill()
        {
            CharacterSkillAssociations = new HashSet<CharacterSkillAssociation>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string Defaults { get; set; }
        public string BaseAttribute { get; set; }

        public virtual ICollection<CharacterSkillAssociation> CharacterSkillAssociations { get; set; }
    }
}
