using System;
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
        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(this);
            EventBus.OnGlossarySelected += OnGlossaryModelSelectedAsync;
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) _jsService.ImagePaster.RegisterImagePasteCanvas();
            base.OnAfterRender(firstRender);
        }

        private IJsFunctionCallerService _jsService;

        public CrudActions SubmitAction { get; set; }
        public GlossaryModel GlossaryEditModel { get; set; } = new GlossaryModel();

        public async Task OnGlossaryModelSelectedAsync(GlossaryModel model)
        {
            GlossaryEditModel = await Http.GetFromJsonAsync<GlossaryModel>(
                string.Format(ApiAddressResources.Glossary_GetImage, model.Id));
            _ = _jsService.ImagePaster.SetImageDataToCanvas(GlossaryEditModel.Image);
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
                    GlossaryEditModel.Image = await _jsService.ImagePaster.GetImageDataFromCanvas();
                    using (var result = await Http.PostAsJsonAsync(
                               ApiAddressResources.Glossary_GetPutPost, GlossaryEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                        GlossaryEditModel = await result.Content.ReadFromJsonAsync<GlossaryModel>().ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Update:
                    GlossaryEditModel.Image = await _jsService.ImagePaster.GetImageDataFromCanvas();
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
            _jsService.ImagePaster.UnregisterImagePasteCanvas();
            _jsService?.Dispose();
            EventBus.OnGlossarySelected -= OnGlossaryModelSelectedAsync;
        }
    }
}
