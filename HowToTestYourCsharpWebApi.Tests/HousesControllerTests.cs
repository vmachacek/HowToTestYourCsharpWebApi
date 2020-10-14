using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class HousesControllerTests : IntegrationTest
    {
        public HousesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task also_test_cars()
        {
            var testScope = Factory.Services.CreateScope();
            var context = testScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var carName = Guid.NewGuid().ToString("D");
            await context.Cars.AddAsync(new Car() {Name = carName});
            await context.SaveChangesAsync();

            var cars = await this.DefaultClient.GetAsync("/cars");
            cars.StatusCode.ShouldBe(HttpStatusCode.OK);

            var content = await cars.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<Car>>(content);

            list.Count.ShouldBe(1);
            list.FirstOrDefault()?.Name.ShouldBe(carName);
        }
        
        [Fact]
        public async Task should_get_all_houses()
        {
            var testScope = Factory.Services.CreateScope();
            var context = testScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var carName = Guid.NewGuid().ToString("D");
            await context.Houses.AddAsync(new House() {Name = carName});
            await context.SaveChangesAsync();

            var houses = await this.DefaultClient.GetAsync("/houses");
            houses.StatusCode.ShouldBe(HttpStatusCode.OK);

            var content = await houses.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<House>>(content);

            list.Count.ShouldBe(1);
            list.FirstOrDefault()?.Name.ShouldBe(carName);
        }
    }
}