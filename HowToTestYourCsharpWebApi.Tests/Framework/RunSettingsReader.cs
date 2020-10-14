using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HowToTestYourCsharpWebApi.Tests.Framework
{
    public class RunSettingsReader
    {
        private Dictionary<string, Action<IWebHostBuilder>> registeredPresets;

        public RunSettingsReader(IEnumerable<TestRunPreset> runPresets)
        {
            this.registeredPresets = new Dictionary<string, Action<IWebHostBuilder>>(
                runPresets.ToDictionary(f => f.EnvironmentName, f => f.ConfigureAction),
                StringComparer.OrdinalIgnoreCase);
        }

        public void Setup(IWebHostBuilder builder)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            if (this.registeredPresets.TryGetValue(env, out var preset))
            {
                preset(builder);
            }
            else
            {
                throw new Exception("no run settings exists for this environment");
            }
        }
    }
}