using System;
using System.Net.Http;
using System.Threading.Tasks;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Client.Shared;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.Features.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddSingleton(new AppStateContainer());

            await builder.Build().RunAsync();
        }
    }
}
