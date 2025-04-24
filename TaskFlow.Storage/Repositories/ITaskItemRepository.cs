using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public interface ITaskItemRepository : IRepository<TaskItem>
{
    IEnumerable<TaskItem> GetByProjectId(int projectId);
}
