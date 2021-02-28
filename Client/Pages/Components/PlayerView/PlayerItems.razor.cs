﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Client.UiComponents;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerItems : ComponentBase, IDisposable
    {
        private CharacterModel _selectedCharacterModel;
        private IJsFunctionCallerService _jsService;

        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(JavascriptGrids.NA, this);
            base.OnInitialized();
        }

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

        public async void UpdateItem()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Item_Delete, ItemEditModel.Id, SelectedCharacterModel.Id)).ConfigureAwait(false))
                    {
                        await _jsService.CheckHttpResponse(result).ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Add:
                    using (var result = await Http.PostAsJsonAsync(
                               string.Format(ApiAddressResources.Item_Post, SelectedCharacterModel.Id),
                               ItemEditModel
                                                                  ).ConfigureAwait(false))
                    {
                        await _jsService.CheckHttpResponse(result).ConfigureAwait(false);
                        ItemEditModel = await result.Content.ReadFromJsonAsync<ItemModel>().ConfigureAwait(false);
                        GetAllItems();
                    }
                    break;

                case CrudActions.Update:
                    using (var result = await Http.DeleteAsync(ApiAddressResources.Item_Put).ConfigureAwait(false))
                    {
                        await _jsService.CheckHttpResponse(result).ConfigureAwait(false);
                    }
                    break;
            }
        }

        public void Dispose()
        {
            _jsService?.Dispose();
        }
    }
}
