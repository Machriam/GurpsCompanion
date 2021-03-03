using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GurpsCompanion.Client.JsInterop
{
    public interface IJsClipboardService
    {
        ValueTask<string> ReadTextAsync();

        ValueTask WriteTextAsync(string text);
    }

    public class JsClipboardService : IJsClipboardService
    {
        private readonly IJSRuntime _jSRuntime;

        public JsClipboardService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public ValueTask<string> ReadTextAsync()
        {
            return _jSRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        }

        public ValueTask WriteTextAsync(string text)
        {
            return _jSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
