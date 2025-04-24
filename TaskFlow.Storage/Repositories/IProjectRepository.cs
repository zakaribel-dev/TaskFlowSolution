using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    IEnumerable<Project> GetByUserId(int userId);
}
