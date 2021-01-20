﻿#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Skill
    {
        public Skill()
        {
        }

        public Skill(SkillModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Value = model.Value;
            Difficulty = model.Difficulty;
            Defaults = model.Defaults;
        }
    }
}