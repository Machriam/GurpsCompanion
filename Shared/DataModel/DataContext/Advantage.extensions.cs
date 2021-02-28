#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Advantage
    {
        public Advantage(AdvantageModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Cost = model.Cost;
        }
    }
}
