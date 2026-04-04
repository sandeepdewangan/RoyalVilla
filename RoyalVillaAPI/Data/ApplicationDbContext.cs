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

        // OnModelCreating - Tell EF Core how to build the database structure from your models.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //HasData - “Add default data to the database when migrations run.”
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Lunxury villa with private pool",
                    Rate = 15000,
                    Sqft = 4000,
                    Occupancy = 6,
                    ImageUrl = "https://cdn-6151b331c1ac189188d8dcd4.closte.com/wp-content/uploads/2021/11/andaman-private-pool-villa-cs-02.jpg",
                    CreatedDate = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc),
                    UpdatedDate = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc),
                },
                 new Villa
                 {
                     Id = 2,
                     Name = "Garden Villa",
                     Details = "Garden villa with garden",
                     Rate = 12000,
                     Sqft = 2000,
                     Occupancy = 6,
                     ImageUrl = "https://cdn-6151b331c1ac189188d8dcd4.closte.com/wp-content/uploads/2021/11/andaman-private-pool-villa-cs-02.jpg",
                     CreatedDate = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc),
                     UpdatedDate = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc),
                 }
                );
        }

    }
}
