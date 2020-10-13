using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HowToTestYourCsharpWebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public CarsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllCars()
        {
            return Ok(await this.databaseContext.Cars.ToListAsync());
        }
    }
}