using Microsoft.EntityFrameworkCore;

namespace HowToTestYourCsharpWebApi.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> settings) : base(settings)
        {
        }
    }
}