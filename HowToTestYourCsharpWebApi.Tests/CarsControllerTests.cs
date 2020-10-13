using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.Database;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class CarsControllerTests : IntegrationTest
    {
        public CarsControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task can_load_all_cars_from_db()
        {
            var testScope = VanillaFactory.Services.CreateScope();
            var context = testScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            context.Cars.RemoveRange(await context.Cars.ToListAsync());

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
    }
}