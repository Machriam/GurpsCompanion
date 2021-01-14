using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Server.DataContext
{
    public partial class Item
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public long Id { get; set; }
        public byte[] Image { get; set; }
        public long Weight { get; set; }
    }
}
