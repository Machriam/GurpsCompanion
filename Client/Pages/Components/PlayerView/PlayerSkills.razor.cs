using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerSkills : ComponentBase
    {
        private CharacterModel _selectedCharacterModel;

        [Parameter]
        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (_selectedCharacterModel == value) return;
                _selectedCharacterModel = value;
                OriginalCharacterModel = value?.Clone();
            }
        }

        public CharacterModel OriginalCharacterModel { get; set; }

        public void UpdateStats()
        {
        }
    }
}
