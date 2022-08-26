using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext:IdentityDbContext<ApiUser>
    {
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options):base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id=1,
                    Name="Turkey",
                    ShortName="TR"
                },
                new Country
                {
                    Id = 2,
                    Name = "England",
                    ShortName = "ENG"
                },
                new Country
                {
                    Id = 3,
                    Name = "United State",
                    ShortName = "USA"
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id=1,
                    Name="Blue Hotel",
                    Rating=4.3,
                    CountryId=1
                },
                 new Hotel
                 {
                     Id = 2,
                     Name = "Hilton Hotel",
                     Rating = 4.5,
                     CountryId = 2
                 }, new Hotel
                 {
                     Id = 3,
                     Name = "NVme Hotel",
                     Rating = 4.0,
                     CountryId = 3
                 }
                );
        }
    }
}
