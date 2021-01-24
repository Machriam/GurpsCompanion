using System.Collections.Generic;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerItems : ComponentBase
    {
        private CharacterModel _selectedCharacterModel;

        [Parameter]
        public IEnumerable<ItemModel> Items { get; set; }

        [Parameter]
        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (_selectedCharacterModel == value) return;
                _selectedCharacterModel = value;
                GetAllItems();
                OriginalCharacterModel = value?.Clone();
            }
        }

        public ItemModel ItemEditModel { get; set; } = new ItemModel();

        public CharacterModel OriginalCharacterModel { get; set; }
        public List<string> ItemNames { get; set; }

        public async void GetAllItems()
        {
            ItemNames = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.GetItemNames).ConfigureAwait(false);
        }

        public void UpdateItem()
        {
        }
    }
}
