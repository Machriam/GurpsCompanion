using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Client.UiComponents;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerAdvantages : ComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(JavascriptGrids.NA, this);
            base.OnInitialized();
        }

        private IJsFunctionCallerService _jsService;
        private CharacterModel _selectedCharacterModel;

        [Parameter]
        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (_selectedCharacterModel == value) return;
                _selectedCharacterModel = value;
                GetAvailableAdvantages();
                OriginalCharacterModel = value?.Clone();
            }
        }

        [Parameter]
        public IEnumerable<AdvantageModel> Advantages { get; set; }

        public AdvantageModel SelectedRow { get; set; }

        public CrudActions SubmitAction { get; set; }

        public IEnumerable<GenericDataListItem> AdvantageNames { get; set; }

        public async void SelectedAdvantageHasChanged(DataListEntry item)
        {
            if (item.SelectedItem != null)
            {
                AdvantageEditModel = await Http.GetFromJsonAsync<AdvantageModel>
                    (string.Format(ApiAddressResources.Advantage_GetAdvantage, item.SelectedItem.GetText)).ConfigureAwait(false);
            }
            else
            {
                AdvantageEditModel = new AdvantageModel() { Name = item.SelectedText };
            }
            StateHasChanged();
        }

        public async void GetAvailableAdvantages()
        {
            var advantages = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.Advantage_GetNames).ConfigureAwait(false);
            AdvantageNames = advantages.Select(n => new GenericDataListItem(n));
            StateHasChanged();
        }

        public long CpSumOfAdvantages => Advantages.Sum(a => a.Level * a.Cost);

        public AdvantageModel AdvantageEditModel { get; set; } = new AdvantageModel();

        public CharacterModel OriginalCharacterModel { get; set; }

        public async void UpdateAdvantages()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Advantage_Delete, AdvantageEditModel.Id, SelectedCharacterModel.Id)).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;

                case CrudActions.Add:
                    using (var result = await Http.PostAsJsonAsync(
                               string.Format(ApiAddressResources.Advantage_Post, SelectedCharacterModel.Id),
                               AdvantageEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                        AdvantageEditModel = await result.Content.ReadFromJsonAsync<AdvantageModel>().ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Update:
                    using (var result = await Http.PutAsJsonAsync(
                        string.Format(ApiAddressResources.Advantage_Put, SelectedCharacterModel.Id),
                        AdvantageEditModel).ConfigureAwait(false))
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
        }
    }
}
