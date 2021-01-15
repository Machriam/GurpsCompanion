using System;
using System.Collections.Generic;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Advantage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Cost { get; set; }
    }
}
