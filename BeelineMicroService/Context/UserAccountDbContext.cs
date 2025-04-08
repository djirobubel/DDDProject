using BeelineMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BeelineMicroService.Context
{
    public class UserAccountDbContext : DbContext
    {
        public DbSet<UserAccountEntity> UserAccounts { get; set; }
        public DbSet<EventEntity> Events { get; set; }

        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccountEntity>().HasData(
                new UserAccountEntity
                {
                    Id = 1,
                    Balance = 100m,
                    IsBlocked = false,
                    Version = 0
                },
                new UserAccountEntity
                {
                    Id = 2,
                    Balance = 50m,
                    IsBlocked = true,
                    Version = 1
                }
            );
        }
    }
}
