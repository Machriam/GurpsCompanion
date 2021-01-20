using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages
{
    public partial class PlayerView : ComponentBase
    {
        public List<CharacterModel> Characters { get; set; }
        private string _selectedCharacterModelName;

        public string SelectedCharacterModelName
        {
            get => _selectedCharacterModelName; set
            {
                if (value == _selectedCharacterModelName) return;
                _selectedCharacterModelName = value;
                SelectedCharacterModel = Characters.Find(c => c.Name == value);
            }
        }

        private CharacterModel _selectedCharacterModel;

        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (value == _selectedCharacterModel) return;
                _selectedCharacterModel = value;
                RetrieveCharacterInformation();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Characters = await Http.GetFromJsonAsync<List<CharacterModel>>(ApiAddressResources.Character_Base).ConfigureAwait(false);
        }

        public void RetrieveCharacterInformation()
        {
        }
    }
}
