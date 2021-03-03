using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GurpsCompanion.Client.JsInterop
{
    public interface IImagePaster
    {
        Task<string> GetImageDataFromCanvas();

        Task RegisterImagePasteCanvas();

        Task SetImageDataToCanvas(string imageUrl);

        Task UnregisterImagePasteCanvas();
    }

    public class ImagePaster<T> : IImagePaster where T : class
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DotNetObjectReference<T> _objectReference;

        public ImagePaster(IJSRuntime jsRuntime, DotNetObjectReference<T> objectReference)
        {
            _jsRuntime = jsRuntime;
            _objectReference = objectReference;
        }

        public async Task RegisterImagePasteCanvas()
        {
            await _jsRuntime.InvokeVoidAsync("imageFunctions.initialize", _objectReference).ConfigureAwait(false);
        }

        public async Task<string> GetImageDataFromCanvas()
        {
            return await _jsRuntime.InvokeAsync<string>("imageFunctions.getImageData");
        }

        public async Task SetImageDataToCanvas(string imageUrl)
        {
            await _jsRuntime.InvokeVoidAsync("imageFunctions.setImageData", imageUrl);
        }

        public async Task UnregisterImagePasteCanvas()
        {
            await _jsRuntime.InvokeVoidAsync("imageFunctions.dispose").ConfigureAwait(false);
        }
    }
}
