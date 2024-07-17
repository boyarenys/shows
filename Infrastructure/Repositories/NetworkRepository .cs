using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class NetworkRepository : Repository<Network>
    {
        public NetworkRepository(AppDbContext context) : base(context)
        {
        }
    }
}
