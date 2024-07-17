using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ExternalsRepository : Repository<Externals>
    {
        public ExternalsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
