using System;
using System.Net.Http;
using HowToTestYourCsharpWebApi.Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HowToTestYourCsharpWebApi.Tests.Framework
{
    public static class TestConfiguration
    {
        public static IServiceCollection WithStubbedOutApis(this IServiceCollection services)
        {
            services.AddTransient<IHttpClientFactory, HttpClientFactoryStub>();
            return services;
        }

        public static IServiceCollection WithInMemoryDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            //one DB per all tests
            var runId = Guid.NewGuid().ToString();
            //services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<T>((options) => { options.UseInMemoryDatabase(runId); });
            return services;
        }
    }
}