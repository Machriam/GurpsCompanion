using System.ComponentModel.DataAnnotations;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.Validation;

namespace GurpsCompanion.Shared.DataModel
{
    public class AdvantageModel
    {
        public AdvantageModel Clone()
        {
            return (AdvantageModel)MemberwiseClone();
        }

        public AdvantageModel()
        {
        }

        public AdvantageModel(Advantage model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Cost = model.Cost;
            Skillable = model.Skillable != 0;
        }

        public long Id { get; set; }

        [StringExists]
        public string Name { get; set; }

        [StringExists]
        public string Description { get; set; }

        [Range(-999, 999)]
        public long Cost { get; set; }

        [AdvantageLevelCheck(ErrorMessage = "Level must be higher than 0 and is only skillable, when the flag is set")]
        public long Level { get; set; }

        public bool Skillable { get; set; }
    }
}
