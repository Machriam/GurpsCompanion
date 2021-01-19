using GurpsCompanion.Server.Core;
using GurpsCompanion.Shared.DataModel.DataContext;
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
            services.AddEntityFrameworkSqlite();
            var options = Configuration.GetSection(ConfigurationOptions.Configuration).Get<ConfigurationOptions>();
            IEnvironmentConfiguration configuration;
            if (_environment.IsDevelopment())
            {
                services.AddSingleton<IEnvironmentConfiguration, DebugConfiguration>();
                configuration = new DebugConfiguration(options);
            }
            else
            {
                services.AddSingleton<IEnvironmentConfiguration, ReleaseConfiguration>();
                configuration = new ReleaseConfiguration(options);
            }
            services.AddSingleton(options);
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(configuration.DatabaseConnection()));
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
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
