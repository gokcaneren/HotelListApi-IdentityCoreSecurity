using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Turkey",
                    ShortName = "TR"
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
        }
    }
}
