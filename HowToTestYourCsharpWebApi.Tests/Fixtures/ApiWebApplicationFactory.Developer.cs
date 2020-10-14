using System;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Tests.Framework;
using Microsoft.AspNetCore.Hosting;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    // THIS FILE IS NOT TRACKED BY SOURCE CONTROL
    // you can make any global adjustments for the test run here
    public partial class ApiWebApplicationFactory
    {
        // here you can add what you need to change
        private Action<IWebHostBuilder> developerSetup = builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.WithInMemoryDatabase<DatabaseContext>();
                services.WithStubbedOutApis();
            });
        };
    }
}