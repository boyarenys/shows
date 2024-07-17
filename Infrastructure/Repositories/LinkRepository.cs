using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class LinkRepository : Repository<Link>
    {
        public LinkRepository(AppDbContext context) : base(context)
        {
        }
    }
}
