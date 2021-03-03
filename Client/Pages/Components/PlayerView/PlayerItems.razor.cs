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
            EventBus.OnItemSelected += SelectedItemChanged;
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
                GetPlayerItemNames();
            }
        }

        public void SelectedItemChanged(long itemId, long itemAssId)
        {
            ItemEditModel = Items.First(i => i.Id == itemId && i.CharacterItemAssId == itemAssId).Clone();
            StateHasChanged();
        }

        public ItemModel ItemEditModel { get; set; } = new ItemModel();

        public CrudActions SubmitAction { get; set; }

        public IEnumerable<IDataListItem> ItemNames { get; set; }

        public async void GetPlayerItemNames()
        {
            var names = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.GetItemNames).ConfigureAwait(false);
            ItemNames = names.Select(n => (IDataListItem)(new ItemModel() { Name = n }));
            StateHasChanged();
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
            StateHasChanged();
        }

        public async void UpdateItem()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Item_Delete, ItemEditModel.CharacterItemAssId)).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;

                case CrudActions.Add:
                    using (var result = await Http.PostAsJsonAsync(
                               string.Format(ApiAddressResources.Item_Post, SelectedCharacterModel.Id),
                               ItemEditModel
                                                                  ).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                        ItemEditModel = await result.Content.ReadFromJsonAsync<ItemModel>().ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Update:
                    using (var result = await Http.PutAsJsonAsync(ApiAddressResources.Item_Put, ItemEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;
            }
            EventBus.InvokeItemChanged();
        }

        public void Dispose()
        {
            _jsService?.Dispose();
            EventBus.OnItemSelected -= SelectedItemChanged;
        }
    }
}
