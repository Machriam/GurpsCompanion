using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Shared.DataModel
{
    public class SkillModel
    {
        public SkillModel()
        {
        }

        public SkillModel(Skill model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Value = model.Value;
            Difficulty = model.Difficulty;
            Defaults = model.Defaults;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Value { get; set; }
        public string Difficulty { get; set; }
        public string Defaults { get; set; }
    }
}
