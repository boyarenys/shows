using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class PreviousEpisodeRepository : Repository<Previousepisode>
    {
        public PreviousEpisodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
