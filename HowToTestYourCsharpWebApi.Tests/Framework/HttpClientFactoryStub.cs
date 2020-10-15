using System;
using System.Net.Http;
using HowToTestYourCsharpWebApi.Tests.Stubs;

namespace HowToTestYourCsharpWebApi.Tests.Framework
{
    //this is place where you define your stubs for HttpClient
    //you can decide which APIs will be faked and which will be real
    public class HttpClientFactoryStub : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            if (name == "Fixer")
            {
                //this will provide canned data for Fixer service - not actual API call will be made
                return new HttpClient(new FixerSimulatedClient());
            }

            throw new NotImplementedException();
        }
    }
}