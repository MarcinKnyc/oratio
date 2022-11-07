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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}