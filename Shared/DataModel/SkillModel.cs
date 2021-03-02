using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.Validation;

namespace GurpsCompanion.Shared.DataModel
{
    public class SkillModel
    {
        public SkillModel Clone()
        {
            return (SkillModel)MemberwiseClone();
        }
        public SkillModel()
        {
        }

        public SkillModel(Skill model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Difficulty = EnumConverter<SkillDifficulties>.ConvertTo(model.Difficulty);
            Defaults = model.Defaults;
            BaseAttribute = EnumConverter<SkillBaseAttributes>.ConvertTo(model.BaseAttribute);
        }

        public long Id { get; set; }

        [StringExists]
        public string Name { get; set; }

        [StringExists]
        public string Description { get; set; }

        public long Value { get; set; }
        public SkillDifficulties Difficulty { get; set; }

        [StringExists]
        public string Defaults { get; set; }

        public SkillBaseAttributes BaseAttribute { get; set; }
    }
}
