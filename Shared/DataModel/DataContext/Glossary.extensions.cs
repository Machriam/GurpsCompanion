#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Glossary
    {
        public Glossary()
        {
        }

        public Glossary(GlossaryModel model)
        {
            Name = model.Name.Trim();
            Description = model.Description.Trim();
            Image = model.Image;
        }
    }
}
