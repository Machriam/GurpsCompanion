namespace GurpsCompanion.Shared.Features.Authentication
{
    public class GlobalPasswordHashModel
    {
        public GlobalPasswordHashModel()
        {
        }

        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
