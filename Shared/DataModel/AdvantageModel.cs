using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Shared.DataModel
{
    public class AdvantageModel
    {
        public AdvantageModel()
        {
        }

        public AdvantageModel(Advantage model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Cost = model.Cost;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Cost { get; set; }
    }
}
