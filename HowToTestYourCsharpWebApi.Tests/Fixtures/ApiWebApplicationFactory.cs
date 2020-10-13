﻿using System;
using System.Net.Http;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Api.Ports;
using HowToTestYourCsharpWebApi.Tests.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Api.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("integrationsettings.json")
                    .Build();

                config.AddConfiguration(integrationConfig);
            });

            // is called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigStub>();
                services.AddTransient<IHttpClientFactory, HttpClientFactoryStub>();
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<DatabaseContext>((options) =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString("N"));
                });
            });
        }
    }
}