using Microsoft.EntityFrameworkCore;
using SO.Data.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
