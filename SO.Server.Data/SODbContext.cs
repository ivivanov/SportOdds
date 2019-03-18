using Microsoft.EntityFrameworkCore;
using SO.Server.Data.Entities;

namespace SO.Server.Data
{
    public class SODbContext : DbContext
    {
        public const string DbName = "SportOddsDb.db";

        public SODbContext(DbContextOptions<SODbContext> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchType>(x =>
            {
                x.HasData(
                    new MatchType() { Id = 1, Name = "PreMatch" },
                    new MatchType() { Id = 2, Name = "Live" }
                );
            });
        }
    }
}
