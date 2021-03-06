﻿#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Item
    {
        public Item(ItemModel model)
        {
            Description = model.Description.Trim();
            Price = model.Price;
            Image = model.Image;
            Weight = model.Weight;
            Name = model.Name.Trim();
        }
    }
}
