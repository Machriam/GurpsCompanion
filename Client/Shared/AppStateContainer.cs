using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Client.Shared
{
    public class AppStateContainer
    {
        public GlobalPasswordHashModel PasswordModel { get; set; } = new GlobalPasswordHashModel();
    }
}
