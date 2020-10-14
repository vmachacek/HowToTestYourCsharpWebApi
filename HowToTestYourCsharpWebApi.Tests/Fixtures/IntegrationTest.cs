using Respawn;
using System.Net.Http;
using HowToTestYourCsharpWebApi.Api;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly Checkpoint _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                "__EFMigrationsHistory"
            },
            WithReseed = true
        };

        protected readonly ApiWebApplicationFactory Factory;

        protected readonly HttpClient DefaultClient;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            Factory = fixture;
            DefaultClient = fixture.CreateClient();

            //if needed, reset the DB
            //_checkpoint.Reset(ConnectionString.It).Wait();
        }
    }
}