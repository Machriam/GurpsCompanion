using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IPasswordCryptologizer, PasswordCryptologizer>();
            builder.Services.AddSingleton<IObjectValidator, ObjectValidator>();
            builder.Services.AddScoped<IJsFunctionCallerServiceFactory, JsFunctionCallerServiceFactory>();

            await builder.Build().RunAsync();
        }
    }
}
