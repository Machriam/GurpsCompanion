using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerGlossary : ComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(this);
            EventBus.OnGlossarySelected += OnGlossaryModelSelected;
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) _jsService.RegisterImagePasteCanvas();
            base.OnAfterRender(firstRender);
        }

        private IJsFunctionCallerService _jsService;

        public CrudActions SubmitAction { get; set; }
        public GlossaryModel GlossaryEditModel { get; set; } = new GlossaryModel();

        public void OnGlossaryModelSelected(GlossaryModel model)
        {
            GlossaryEditModel = model;
            StateHasChanged();
        }

        public async void UpdateEntry()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Glossary_Delete, GlossaryEditModel.Id)).ConfigureAwait(false))
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
            StateHasChanged();
            EventBus.InvokeGlossaryChanged();
        }

        public void Dispose()
        {
            _jsService.UnregisterImagePasteCanvas();
            _jsService?.Dispose();
            EventBus.OnGlossarySelected -= OnGlossaryModelSelected;
        }
    }
}
