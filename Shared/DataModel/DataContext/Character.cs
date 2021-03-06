﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Character
    {
        public Character()
        {
            CharacterAdvantageAssociations = new HashSet<CharacterAdvantageAssociation>();
            CharacterItemAssociations = new HashSet<CharacterItemAssociation>();
            CharacterSkillAssociations = new HashSet<CharacterSkillAssociation>();
        }

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

        public virtual ICollection<CharacterAdvantageAssociation> CharacterAdvantageAssociations { get; set; }
        public virtual ICollection<CharacterItemAssociation> CharacterItemAssociations { get; set; }
        public virtual ICollection<CharacterSkillAssociation> CharacterSkillAssociations { get; set; }
    }
}
