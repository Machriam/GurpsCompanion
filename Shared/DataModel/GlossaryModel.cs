#nullable disable

using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.Validation;

namespace GurpsCompanion.Shared.DataModel
{
    public class GlossaryModel
    {
        public GlossaryModel Clone()
        {
            return (GlossaryModel)MemberwiseClone();
        }

        public GlossaryModel()
        {
        }

        public GlossaryModel(Glossary model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }

        public long Id { get; set; }

        [StringExists]
        public string Name { get; set; }

        [StringExists]
        public string Description { get; set; }

        public string Image { get; set; }
    }
}
