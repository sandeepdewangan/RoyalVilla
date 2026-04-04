using Microsoft.EntityFrameworkCore;
using RoyalVillaAPI.Data.Models;

namespace RoyalVillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Villa> Villa { get; set; }

    }
}
