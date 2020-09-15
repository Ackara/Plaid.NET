using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Plaid.Link
{
    public class Startup
    {
        public Startup(IWebHostEnvironment host)
        {
            _rootDirectory = host.ContentRootPath;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Arguments

            var config = new ConfigurationBuilder()
                .SetBasePath(_rootDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
            services.AddSingleton<IConfiguration>(config);

            // Routing

            services.AddControllers().AddJsonOptions((builder) =>
            {
                builder.JsonSerializerOptions.IgnoreNullValues = true;
                builder.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                builder.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                builder.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{Controller=Home}/{Action=Index}");
            });
        }

        #region Backing Members

        private readonly string _rootDirectory;

        #endregion Backing Members
    }
}
