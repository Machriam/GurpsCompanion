using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Client.ExtensionMethods
{
    public static class JsRuntimeExtensions
    {
        public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message)
        {
            return jsRuntime.InvokeAsync<bool>("confirm", message);
        }

        public async static ValueTask<bool> ConfirmDeletion(this IJSRuntime jsRuntime, string item)
        {
            return await jsRuntime.InvokeAsync<string>("prompt", $"If you really want to delete: '{item}' then enter 'delete'")
                .ConfigureAwait(false) == "delete";
        }

        public async static ValueTask<string> GetUserString(this IJSRuntime jsRuntime, string question)
        {
            return await jsRuntime.InvokeAsync<string>("prompt", question).ConfigureAwait(false);
        }

        public async static ValueTask<bool> ConfirmPassword(this IJSRuntime jsRuntime, GlobalPasswordHashModel model, IPasswordCryptologizer cryptologizer, string description = "")
        {
            var result = await jsRuntime.InvokeAsync<string>("prompt", description + Environment.NewLine + "Please enter the password!").ConfigureAwait(false);
            if (result == null) return false;
            var hash = cryptologizer.EncryptString(cryptologizer.EncryptString(result) + model.Salt);
            return hash == model.Hash;
        }

        public async static Task Prompt(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("alert", message).ConfigureAwait(false);
        }
    }
}
