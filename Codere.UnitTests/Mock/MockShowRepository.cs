using Domain.Entities;
using Domain.Interface;
using Moq;
using System.Linq.Expressions;

namespace Codere.UnitTests.Mock
{
    public static class MockShowRepository
    {
        public static Mock<IRepository<Show>> Get()
        {
            var mockRepo = new Mock<IRepository<Show>>();

            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(new List<Show>());

            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => new Show { Id = id });

            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Show>()))
                    .Returns(Task.CompletedTask);

            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Show>()))
                    .Returns(Task.CompletedTask);

            mockRepo.Setup(repo => repo.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

            mockRepo.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                    .ReturnsAsync((Expression<Func<Show, bool>> predicate) => null);

            return mockRepo;
        }
    }
}
