using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.UiComponents;
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
        public CrudActions SubmitAction { get; set; }
        public IEnumerable<IDataListItem> ItemNames { get; set; }

        public async void GetAllItems()
        {
            var names = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.GetItemNames).ConfigureAwait(false);
            ItemNames = names.Select(n => (IDataListItem)(new ItemModel() { Name = n }));
        }

        public async void InputHasChanged(DataListEntry item)
        {
            if (item.SelectedItem != null)
            {
                ItemEditModel = await Http.GetFromJsonAsync<ItemModel>
                    (string.Format(ApiAddressResources.GetItem, item.SelectedItem.GetText)).ConfigureAwait(false);
            }
            else
            {
                ItemEditModel = new ItemModel() { Name = item.SelectedText };
            }
        }

        public void UpdateItem()
        {
        }
    }
}
