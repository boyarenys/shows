using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
