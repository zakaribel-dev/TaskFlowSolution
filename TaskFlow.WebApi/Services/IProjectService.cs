
using TaskFlow.Domain.Entities;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync(int userId);
    Task<Project> CreateProjectAsync(Project project, int userId);
    Task<Project> GetProjectByIdAsync(int id, int userId);
    Task UpdateProjectAsync(Project project, int userId);
    Task DeleteProjectAsync(int id, int userId);
}
