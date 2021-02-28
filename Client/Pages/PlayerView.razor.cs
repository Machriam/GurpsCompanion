using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.FeatureModels;

namespace GurpsCompanion.Client.Pages
{
    public partial class PlayerView : ComponentBase, IDisposable
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
            Characters = await Http.GetFromJsonAsync<List<CharacterModel>>(string.Format(ApiAddressResources.Character_Base,
                StateContainer.PasswordModel.Hash, StateContainer.PasswordModel.Salt)).ConfigureAwait(false);
            EventBus.OnItemChanged += EventBus_OnItemAdded;
        }

        private void EventBus_OnItemAdded()
        {
            RetrieveCharacterInformation();
        }

        public async void RetrieveCharacterInformation()
        {
            CharacterInformation = null;
            if (SelectedCharacterModel == null) return;
            CharacterInformation = await Http.GetFromJsonAsync<CharacterInformationModel>
                (string.Format(ApiAddressResources.GetCharacterInformation, SelectedCharacterModel.Id)).ConfigureAwait(false);
            StateHasChanged();
        }

        public void Dispose()
        {
            EventBus.OnItemChanged -= EventBus_OnItemAdded;
        }
    }
}
