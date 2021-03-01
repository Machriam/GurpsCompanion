using System.ComponentModel.DataAnnotations;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.Validation;

namespace GurpsCompanion.Shared.DataModel
{
    public class ItemModel : ModelBase, IDataListItem
    {
        public ItemModel Clone()
        {
            return (ItemModel)MemberwiseClone();
        }

        public string GetText => Name;

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

        public bool Wearing { get; set; }
        public long Count { get; set; }

        [Required]
        [StringExists]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public long Id { get; set; }
        public byte[] Image { get; set; }

        [Required]
        public double Weight { get; set; }
    }
}
