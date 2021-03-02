#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Advantage
    {
        public Advantage(AdvantageModel model)
        {
            Name = model.Name.Trim();
            Description = model.Description.Trim();
            Cost = model.Cost;
            Skillable = model.Skillable ? 1 : 0;
        }
    }
}
