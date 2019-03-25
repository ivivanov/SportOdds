using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SO.Data.Migrations
{
    public class MigrationsContextFactory : IDesignTimeDbContextFactory<SODbContext>
    {
        public SODbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODbContext>();
            optionsBuilder.UseSqlite(@"Data Source=..\SQLiteDatabase\SportOddsDb.db");

            return new SODbContext(optionsBuilder.Options);
        }
    }
}
