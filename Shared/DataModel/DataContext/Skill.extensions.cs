#nullable disable

using GurpsCompanion.Shared.Extensions;

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Skill
    {
        public Skill(SkillModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Value = model.Value;
            Difficulty = model.Difficulty.GetDescription();
            Defaults = model.Defaults;
            BaseAttribute = model.BaseAttribute.GetDescription();
        }
    }
}
