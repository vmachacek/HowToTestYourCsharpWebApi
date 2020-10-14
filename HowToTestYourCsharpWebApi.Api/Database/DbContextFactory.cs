using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HowToTestYourCsharpWebApi.Api.Database
{
    public class DbContextFactory: IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Persist Security Info=True;Server=foo;Database=bar;User ID=foo;Password=bar");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
   
}