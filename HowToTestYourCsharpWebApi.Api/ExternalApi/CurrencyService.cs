using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HowToTestYourCsharpWebApi.Api.ExternalApi
{
    public class CurrencyService
    {
        private readonly IMemoryCache memoryCache;
        private readonly HttpClient _httpClient;

        private static string CacheKey = "ExchangeRates";

        public CurrencyService(IMemoryCache memoryCache, IHttpClientFactory httpClient)
        {
            this.memoryCache = memoryCache;
            _httpClient = httpClient.CreateClient("Fixer");
        }

        public async Task<ConversionData> GetRates()
        {
            return await this.memoryCache.GetOrCreateAsync(CacheKey,
                async (entry) =>
                {
                    try
                    {
                        var fixer = await _httpClient.GetStringAsync(
                            "http://data.fixer.io/api/latest?access_key=a3d905cc41626b4d4003a7d718ad5700&format=1");

                        var fixerResponse = JsonConvert.DeserializeObject<ConversionData>(fixer);

                        entry.SetOptions(new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(10)));
                        return fixerResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });
        }


        private float GetPropValue(object src, string propName)
        {
            return Convert.ToSingle(src.GetType().GetProperty(propName)?.GetValue(src, null));
        }

        public async Task<Money> ConvertToEurAsync(Money objPrice)
        {
            if (objPrice.Currency == "EUR")
                return objPrice;

            var rates = await GetRates();
            var value = GetPropValue(rates.Rates, objPrice.Currency.ToUpper());

            return new Money("EUR", objPrice.Amount / (decimal) value);
        }
    }
}