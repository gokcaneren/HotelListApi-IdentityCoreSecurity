using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListingDbContext _context;
        public CountryRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(h => h.Hotels).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
