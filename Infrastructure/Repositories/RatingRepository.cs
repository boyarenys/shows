using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class RatingRepository : Repository<Rating>
    {
        public RatingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
