using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SO.Server.Data
{
    public class SOContextFactory : IDesignTimeDbContextFactory<SODbContext>
    {
        public SODbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODbContext>();
            optionsBuilder.UseSqlite(SODbContext.ConnectionString);

            return new SODbContext(optionsBuilder.Options);
        }
    }
}
