using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.FeatureModels;
using Microsoft.AspNetCore.Components;

namespace GurpsCompanion.Client.Pages
{
    public partial class PlayerView : ComponentBase
    {
        public void SelectedDataListItemChanged(IDataListItem item)
        {
            SelectedCharacterModel = item == null ? null : (CharacterModel)item;
        }

        public IEnumerable<IDataListItem> DataListItems => Characters.Cast<IDataListItem>();
        public CharacterInformationModel CharacterInformation { get; set; }
        public List<CharacterModel> Characters { get; set; }

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
            if (SelectedCharacterModel == null) return;
            CharacterInformation = await Http.GetFromJsonAsync<CharacterInformationModel>
                (ApiAddressResources.GetCharacterInformation + "?id=" + SelectedCharacterModel.Id).ConfigureAwait(false);
            StateHasChanged();
        }
    }
}
