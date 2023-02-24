using GurpsCompanion.Server.Controllers;
using GurpsCompanion.Server.Core;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.Features.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GurpsCompanion.Server
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddEntityFrameworkSqlite();
            var options = Configuration.GetSection(ConfigurationOptions.Configuration).Get<ConfigurationOptions>();
            var configuration = _environment.IsDevelopment()
                ? new DebugConfiguration(options)
                : (IEnvironmentConfiguration)new ReleaseConfiguration(options);
            services.AddSingleton(configuration);
            services.AddSingleton(options);
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(configuration.DatabaseConnection));
            services.AddSingleton<IAuthenticationVerifier, AuthenticationVerifier>();
            services.AddSingleton<IPasswordCryptologizer, PasswordCryptologizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<FighterHub>(IFighterWeightNotificationClient.HubConnection);
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
