using System;
using Microsoft.AspNetCore.Hosting;

namespace HowToTestYourCsharpWebApi.Tests.Framework
{
    public class TestRunPreset
    {
        public string EnvironmentName { get; set; }
        public Action<IWebHostBuilder> ConfigureAction { get; set; }

        public TestRunPreset(string environmentName, Action<IWebHostBuilder> configureAction)
        {
            EnvironmentName = environmentName;
            ConfigureAction = configureAction;
        }
    }
}