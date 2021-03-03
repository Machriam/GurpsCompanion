#nullable disable

using GurpsCompanion.Shared.Extensions;

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Skill
    {
        public Skill(SkillModel model)
        {
            Name = model.Name.Trim();
            Description = model.Description.Trim();
            Difficulty = model.Difficulty.GetDescription();
            Defaults = model.Defaults.Trim();
            BaseAttribute = model.BaseAttribute.GetDescription();
        }
    }
}
