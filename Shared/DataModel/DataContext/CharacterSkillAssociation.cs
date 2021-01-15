using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class CharacterSkillAssociation
    {
        public long CharacterFk { get; set; }
        public long SkillFk { get; set; }

        public virtual Character CharacterFkNavigation { get; set; }
        public virtual Skill SkillFkNavigation { get; set; }
    }
}
