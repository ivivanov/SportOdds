using Microsoft.EntityFrameworkCore;
using SO.Server.Data.Entities;

namespace SO.Server.Data
{
    public class SODbContext : DbContext
    {
        public const string ConnectionString = "Data Source=SportOddsDb.db";

        public SODbContext(DbContextOptions<SODbContext> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed data
        }
    }
}
