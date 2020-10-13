using System;
using System.Net.Http;

namespace HowToTestYourCsharpWebApi.Tests.Stubs
{
    public class HttpClientFactoryStub : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            if (name == "Fixer")
            {
                return new HttpClient(new FixerSimulatedClient());
            }

            throw new NotImplementedException();
        }
    }
}