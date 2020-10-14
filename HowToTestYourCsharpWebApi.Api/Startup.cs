using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.Adapters;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Api.ExternalApi;
using HowToTestYourCsharpWebApi.Api.Ports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HowToTestYourCsharpWebApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DatabaseContext>(ctx => { ctx.UseSqlServer(ConnectionString.It); });

            services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigService>();

            services.AddTransient<CurrencyService>();

            services.AddHttpClient();

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            using var scope = app.ApplicationServices.CreateScope();
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (db.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    db.Database.Migrate();
                }
            }
        }
    }
}