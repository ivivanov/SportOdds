using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SO.Data
{
    public class SOContextFactory : IDesignTimeDbContextFactory<SODbContext>
    {
        public SODbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODbContext>();
            optionsBuilder.UseSqlite(@"Data Source=..\SQLiteDatabase\SportOddsDb.db");

            return new SODbContext(optionsBuilder.Options);
        }
    }
}
