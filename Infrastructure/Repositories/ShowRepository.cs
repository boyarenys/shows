using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ShowRepository : Repository<Show>
    {
        public ShowRepository(AppDbContext context) : base(context)
        {
        }
    }
}
