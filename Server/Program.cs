using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GurpsCompanion.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#pragma warning disable DF0001 // Marks undisposed anonymous objects from method invocations.
            CreateHostBuilder(args).Build().Run();
#pragma warning restore DF0001 // Marks undisposed anonymous objects from method invocations.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}