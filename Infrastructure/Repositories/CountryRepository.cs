using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
