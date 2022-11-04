using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oratio.Models;

namespace Oratio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ExampleDatabaseModel> Models { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}