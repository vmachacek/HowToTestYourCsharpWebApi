using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.ExternalApi;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class CurrencyControllerTest : IntegrationTest
    {
        private CurrencyService currencyService;

        public CurrencyControllerTest(ApiWebApplicationFactory fixture) : base(fixture)
        {
            this.currencyService = fixture.Services.GetRequiredService<CurrencyService>();
        }

        [Fact]
        public async Task should_convert_used_to_eur()
        {
            var payload = JsonConvert.SerializeObject(new Money("USD", 1));
            var response = await DefaultClient.PostAsync("/money",
                new StringContent(payload, Encoding.UTF8, "application/json"));
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}