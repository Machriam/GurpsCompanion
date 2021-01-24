using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.FeatureModels;

namespace GurpsCompanion.Client.Pages
{
    public partial class PlayerView : ComponentBase
    {
        public CharacterInformationModel CharacterInformation { get; set; }
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
                CharacterEditModel = value?.Clone();
                RetrieveCharacterInformation();
            }
        }

        public CharacterModel CharacterEditModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Characters = await Http.GetFromJsonAsync<List<CharacterModel>>(ApiAddressResources.Character_Base).ConfigureAwait(false);
        }

        public async void RetrieveCharacterInformation()
        {
            CharacterInformation = null;
            CharacterInformation = await Http.GetFromJsonAsync<CharacterInformationModel>
                (ApiAddressResources.GetCharacterInformation + "?id=" + SelectedCharacterModel.Id).ConfigureAwait(false);
            StateHasChanged();
        }
    }
}
