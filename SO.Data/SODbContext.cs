using Microsoft.EntityFrameworkCore;
using SO.Data.Entities;
using Z.EntityFramework.Extensions;

namespace SO.Data
{
    public class SODbContext : DbContext
    {
        public const string ConnectionString = @"Data Source=..\SQLiteDatabase\SportOddsDb.db";

        public SODbContext(DbContextOptions<SODbContext> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Odd> Odds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            EntityFrameworkManager.ContextFactory = context =>
            {
                var builder = new DbContextOptionsBuilder<SODbContext>();
                builder.UseSqlite(ConnectionString);
                return new SODbContext(builder.Options);
            };

            optionsBuilder.UseSqlite(SODbContext.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
