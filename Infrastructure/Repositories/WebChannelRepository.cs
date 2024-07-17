using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class WebChannelRepository : Repository<WebChannel>
    {
        public WebChannelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
