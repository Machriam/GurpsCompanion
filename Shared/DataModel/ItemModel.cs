using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Shared.DataModel
{
    public class ItemModel : ModelBase
    {
        public ItemModel Clone()
        {
            return (ItemModel)MemberwiseClone();
        }

        public ItemModel()
        {
        }

        public ItemModel(Item model)
        {
            Description = model.Description;
            Price = model.Price;
            Id = model.Id;
            Image = model.Image;
            Weight = model.Weight;
            Name = model.Name;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public long Id { get; set; }
        public byte[] Image { get; set; }
        public long Weight { get; set; }
    }
}
