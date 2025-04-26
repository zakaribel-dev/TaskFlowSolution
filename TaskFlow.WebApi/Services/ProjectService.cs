using TaskFlow.Domain.Entities;
using TaskFlow.Storage.Repositories;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepo;

    public ProjectService(IProjectRepository projectRepo)
    {
        _projectRepo = projectRepo;
    }

    public Task<List<Project>> GetAllProjectsAsync(int userId)
    {
        var projects = _projectRepo.GetByUserId(userId).ToList();
        return Task.FromResult(projects);
    }

    public Task<Project> CreateProjectAsync(Project project, int userId)
    {
        if (project.UserId != userId) throw new UnauthorizedAccessException();

        _projectRepo.Add(project);
        _projectRepo.SaveChanges();
        return Task.FromResult(project);
    }

    public Task<Project> GetProjectByIdAsync(int id, int userId)
    {
        var project = _projectRepo.GetById(id);
        if (project == null || project.UserId != userId) throw new UnauthorizedAccessException();

        return Task.FromResult(project);
    }

    public Task UpdateProjectAsync(Project project, int userId)
    {
        if (project.UserId != userId) throw new UnauthorizedAccessException();

        _projectRepo.Update(project);
        _projectRepo.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteProjectAsync(int id, int userId)
    {
        var project = _projectRepo.GetById(id);
        if (project == null || project.UserId != userId) throw new UnauthorizedAccessException();

        _projectRepo.Delete(id);
        _projectRepo.SaveChanges();
        return Task.CompletedTask;
    }
}
