using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.Core;
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
        public IEnumerable<IDataListItem> ItemNames { get; set; }

        public async void GetAllItems()
        {
            var names = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.GetItemNames).ConfigureAwait(false);
            ItemNames = names.Select(n => (IDataListItem)(new ItemModel() { Name = n }));
        }

        public async void SelectedItemChanged(IDataListItem item)
        {
            ItemEditModel = await Http.GetFromJsonAsync<ItemModel>
                (ApiAddressResources.GetItem + "?name=" + item.GetText).ConfigureAwait(false);
        }

        public void UpdateItem()
        {
        }
    }
}
