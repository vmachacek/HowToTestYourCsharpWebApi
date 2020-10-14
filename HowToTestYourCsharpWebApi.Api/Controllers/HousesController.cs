using System.Threading.Tasks;
using HowToTestYourCsharpWebApi.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HowToTestYourCsharpWebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HousesController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public HousesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllHouses()
        {
            return Ok(await this.databaseContext.Houses.ToListAsync());
        }
    }
}