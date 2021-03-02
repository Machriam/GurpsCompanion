using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerGlossary : ComponentBase, IDisposable
    {
        protected override async Task OnInitializedAsync()
        {
            _jsService = JsServiceFactory.Create(JavascriptGrids.NA, this);
            GlossaryEntries = await Http.GetFromJsonAsync<IEnumerable<GlossaryModel>>
                (ApiAddressResources.Glossary_GetPutPost).ConfigureAwait(false);
            _ = base.OnInitializedAsync();
        }

        private IJsFunctionCallerService _jsService;
        public IEnumerable<GlossaryModel> GlossaryEntries { get; set; }

        public GlossaryModel SelectedRow { get; set; }

        public CrudActions SubmitAction { get; set; }
        public GlossaryModel GlossaryEditModel { get; set; } = new GlossaryModel();

        public async void UpdateEntry()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Glossary_Delete, SelectedRow.Id)).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;

                case CrudActions.Add:
                    using (var result = await Http.PostAsJsonAsync(
                               ApiAddressResources.Glossary_GetPutPost, GlossaryEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                        GlossaryEditModel = await result.Content.ReadFromJsonAsync<GlossaryModel>().ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Update:
                    using (var result = await Http.PutAsJsonAsync(
                        ApiAddressResources.Glossary_GetPutPost, GlossaryEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;
            }
            GlossaryEntries = await Http.GetFromJsonAsync<IEnumerable<GlossaryModel>>
                (ApiAddressResources.Glossary_GetPutPost).ConfigureAwait(false);
            StateHasChanged();
        }

        public void Dispose()
        {
            _jsService?.Dispose();
        }
    }
}
