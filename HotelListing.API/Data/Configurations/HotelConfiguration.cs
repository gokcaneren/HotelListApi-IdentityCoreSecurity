using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Blue Hotel",
                    Rating = 4.3,
                    CountryId = 1
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
