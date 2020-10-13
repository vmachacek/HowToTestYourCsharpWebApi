using HowToTestYourCsharpWebApi.Api;
using HowToTestYourCsharpWebApi.Api.Ports;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class WeatherForecastControllerTests : IntegrationTest
    {
        public WeatherForecastControllerTests(ApiWebApplicationFactory fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task Get_Should_Return_Forecast()
        {
            var response = await DefaultClient.GetAsync("/weatherforecast");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(
                await response.Content.ReadAsStringAsync()
            );
            forecast.Length.ShouldBe(7);
        }

        [Fact]
        public async Task Get_Should_ResultInABadRequest_When_ConfigIsInvalid()
        {
            // technique like this is used for ad-hoc set up of the test controller, during development
            var client = VanillaFactory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services
                            .AddTransient<IWeatherForecastConfigService, InvalidWeatherForecastConfigStub>();
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions());

            var response = await client.GetAsync("/weatherforecast");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        public class InvalidWeatherForecastConfigStub : IWeatherForecastConfigService
        {
            public int NumberOfDays() => -3;
        }
    }
}