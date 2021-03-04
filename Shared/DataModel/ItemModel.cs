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
            Weight = model.Weight;
            Name = model.Name;
        }

        public bool Equipped { get; set; }

        [Range(0, long.MaxValue)]
        public long Count { get; set; }

        [Required]
        [StringExists]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public double Price { get; set; }

        public long Id { get; set; }
        public string Image { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public double Weight { get; set; }

        public long CharacterItemAssId { get; set; }
    }
}
