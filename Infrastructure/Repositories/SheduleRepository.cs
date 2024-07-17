using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class SheduleRepository : Repository<Schedule>
    {
        public SheduleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
