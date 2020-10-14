using System;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Tests.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    public partial class ApiWebApplicationFactory : WebApplicationFactory<Api.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var presets = new TestRunPreset[]
            {
                // for local development we stub our APIs and add InmemoryDB
                //this can be changed per-test basis when we work on certain aspects
                new TestRunPreset("development", developerSetup),
                
                //staging should be same as prod
                new TestRunPreset("staging", hostBuilder => { }), 
                
                //test env -  with real DB but stubbed out services
                new TestRunPreset("test", hostBuilder =>
                {
                    hostBuilder.ConfigureServices(services =>
                    {
                        services.WithStubbedOutApis();
                    });
                }),
            };
            var runSettings = new RunSettingsReader(presets);
            runSettings.Setup(builder);
        }
    }
}