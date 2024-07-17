using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class SelfRepository : Repository<Self>
    {
        public SelfRepository(AppDbContext context) : base(context)
        {
        }
    }
}
