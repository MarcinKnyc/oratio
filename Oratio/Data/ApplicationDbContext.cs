using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oratio.Models;

namespace Oratio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Church> Churches { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DbSet<Intention> Intentions { get; set; }
        public DbSet<MassGenerationRule> MassGenerationRules { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parish>()
                .HasData(
                    new Parish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fejk",
                        Dedicated = "Żadna"
                    }
                );
        }
        public DbSet<Oratio.Models.Mass> Mass { get; set; }
    }
}