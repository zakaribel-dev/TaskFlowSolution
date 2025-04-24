using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public class ProjectRepository(TaskFlowDbContext context) : IProjectRepository
{
    private readonly TaskFlowDbContext _context = context;

    public IEnumerable<Project> GetAll() =>
        _context.Projects.Include(p => p.Tasks).ToList();

    public Project? GetById(int id) =>
        _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);

    public IEnumerable<Project> GetByUserId(int userId) =>
        _context.Projects.Where(p => p.UserId == userId).Include(p => p.Tasks).ToList();

    public void Add(Project entity) => _context.Projects.Add(entity);

    public void Update(Project entity) => _context.Projects.Update(entity);

    public void Delete(int id)
    {
        var project = _context.Projects.Find(id);
        if (project is not null)
            _context.Projects.Remove(project);
    }

    public void SaveChanges() => _context.SaveChanges();
}
