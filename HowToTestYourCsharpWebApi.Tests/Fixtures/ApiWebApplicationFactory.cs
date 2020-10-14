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
            var presets = new RunPreSet[]
            {
                // for local development we stub our APIs and add InmemoryDB
                //this can be changed per-test basis when we work on certain aspects
                new RunPreSet("development", developerSetup),
                
                //staging should be same as prod
                new RunPreSet("staging", hostBuilder => { }), 
                
                //uat with real DB but stubbed out services
                new RunPreSet("uat", hostBuilder =>
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