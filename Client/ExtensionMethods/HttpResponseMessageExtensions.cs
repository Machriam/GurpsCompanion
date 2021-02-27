using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GurpsCompanion.Client.ExtensionMethods
{
    public static class HttpResponseMessageExtensions
    {
        public static async ValueTask<bool> CheckIfResponseIsOK(this HttpResponseMessage message, IJSRuntime jSRuntime, string potentialReason = "")
        {
            if (!message.IsSuccessStatusCode)
            {
                var reason = await message.Content.ReadAsStringAsync();
                await jSRuntime.Prompt(
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + Environment.NewLine +
                    reason.Split("\r\n")[0] + Environment.NewLine +
                    potentialReason + string.Concat(Enumerable.Repeat(Environment.NewLine, 3)) +
                    "Developer Information:" + message.ReasonPhrase + Environment.NewLine + reason
                                      ).ConfigureAwait(false);
                return false;
            }
            return true;
        }
    }
}
