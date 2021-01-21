using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerStats : ComponentBase
    {
        [Parameter]
        public CharacterModel SelectedCharacterModel { get; set; }
    }
}
