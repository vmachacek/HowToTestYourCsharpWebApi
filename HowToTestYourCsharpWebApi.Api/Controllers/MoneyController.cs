using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.ExternalApi;
using Microsoft.AspNetCore.Mvc;

namespace HowToTestYourCsharpWebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyController : ControllerBase
    {
        private readonly CurrencyService currencyService;

        public MoneyController(CurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpPost]
        public async Task<IActionResult> ExchangeToEur([FromBody] Money payload)
        {
            return Ok(await this.currencyService.ConvertToEurAsync(payload));
        }
    }
}