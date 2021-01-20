#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
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
