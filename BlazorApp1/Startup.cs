using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp1.Data;
using Database;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1
{
    public class Startup
    {
        public string rootFolder;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddDbContext<SampleContext>(
                options => options.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={rootFolder}\\Data\\Database1.mdf;Integrated Security=True"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Content root path = " + Configuration.GetValue<string>(WebHostDefaults.ContentRootKey));
            Console.WriteLine("Current dir = " + System.IO.Directory.GetCurrentDirectory());
            rootFolder = env.ContentRootPath.Substring(0, env.ContentRootPath.LastIndexOf(@"\BlazorApp1\", StringComparison.Ordinal) + @"\BlazorApp1".Length);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
