using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SO.Server.Data
{
    public class SOContextFactory : IDesignTimeDbContextFactory<SODbContext>
    {
        public SODbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODbContext>();
            optionsBuilder.UseSqlite($"Data Source=Databases/{SODbContext.DbName}");

            return new SODbContext(optionsBuilder.Options);
        }
    }
}
