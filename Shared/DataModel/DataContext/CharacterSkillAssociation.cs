using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class CharacterSkillAssociation
    {
        public long CharacterFk { get; set; }
        public long SkillFk { get; set; }
        public long Id { get; set; }
        public long Value { get; set; }

        public virtual Character CharacterFkNavigation { get; set; }
        public virtual Skill SkillFkNavigation { get; set; }
    }
}
