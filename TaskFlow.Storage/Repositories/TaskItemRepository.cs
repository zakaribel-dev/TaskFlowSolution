using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public class TaskItemRepository(TaskFlowDbContext context) : ITaskItemRepository
{
    private readonly TaskFlowDbContext _context = context;

    public IEnumerable<TaskItem> GetAll() =>
        _context.Tasks.Include(t => t.Project).ToList();

    public TaskItem? GetById(int id) =>
        _context.Tasks.Include(t => t.Project).FirstOrDefault(t => t.Id == id);

    public IEnumerable<TaskItem> GetByProjectId(int projectId) =>
        _context.Tasks.Where(t => t.ProjectId == projectId).Include(t => t.Project).ToList();

    public void Add(TaskItem entity) => _context.Tasks.Add(entity);

    public void Update(TaskItem entity) => _context.Tasks.Update(entity);

    public void Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task is not null)
            _context.Tasks.Remove(task);
    }

    public void SaveChanges() => _context.SaveChanges();
}
