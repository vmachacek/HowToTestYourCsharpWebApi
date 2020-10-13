using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.ExternalApi;
using Newtonsoft.Json;

namespace HowToTestYourCsharpWebApi.Tests.Stubs
{
    public class FixerSimulatedClient : HttpMessageHandler
    {
        public Uri LastUrl { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.LastUrl = request.RequestUri;

            //here comes canned data
            return Task.FromResult(Json(new ConversionData()
            {
                Rates = new Rates()
                {
                    USD = 1
                }
            }));
        }

        private static HttpResponseMessage Json(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(statusCode)
                {Content = new StringContent(JsonConvert.SerializeObject(result))};
        }
    }
}