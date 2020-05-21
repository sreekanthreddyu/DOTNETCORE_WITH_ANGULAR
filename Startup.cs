using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg=> 
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnString"));
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<DutchSeeder>();
            services.AddTransient<IMailService, NullMailService>();
            services.AddScoped<IDutchRepository, DutchRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            

            app.UseStaticFiles();

            app.UseNodeModules();
            app.UseRouting();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback", "{Controller}/{Action}/{id?}",
                    new { controller = "App", Action = "Index" });
                cfg.MapRazorPages();
            });

           


        }
    }
}
  