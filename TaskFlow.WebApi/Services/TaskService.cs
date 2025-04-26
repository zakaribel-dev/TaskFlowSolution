using TaskFlow.WebApi.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Storage.Repositories;

public class TaskService : ITaskService
{
    private readonly ITaskItemRepository _taskRepo;
    private readonly IProjectRepository _projectRepo;

    public TaskService(ITaskItemRepository taskRepo, IProjectRepository projectRepo)
    {
        _taskRepo = taskRepo;
        _projectRepo = projectRepo;
    }

    public Task<List<TaskItem>> GetAllTasksAsync(int userId)
    {
        var userProjects = _projectRepo.GetByUserId(userId).Select(p => p.Id).ToList();
        var tasks = _taskRepo.GetAll().Where(t => userProjects.Contains(t.ProjectId)).ToList();
        return Task.FromResult(tasks);
    }

    public Task<TaskItem> GetTaskByIdAsync(int id, int userId)
    {
        var task = _taskRepo.GetById(id);
        if (task == null || task.Project.UserId != userId) throw new UnauthorizedAccessException();
        return Task.FromResult(task);
    }

    public Task<TaskItem> CreateTaskAsync(TaskCreateDto dto, int userId)
    {
        var project = _projectRepo.GetById(dto.ProjectId);
        if (project == null || project.UserId != userId) throw new UnauthorizedAccessException();

        var task = new TaskItem
        {
            Title = dto.Title ?? string.Empty,
            Status = dto.Status,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId,
            Commentaires = dto.Commentaires ?? string.Empty
        };

        _taskRepo.Add(task);
        _taskRepo.SaveChanges();
        return Task.FromResult(task);
    }

    public Task UpdateTaskAsync(int id, TaskUpdateDto dto, int userId)
    {
        var task = _taskRepo.GetById(id);
        if (task == null || task.Project.UserId != userId) throw new UnauthorizedAccessException();

        task.Title = dto.Title ?? string.Empty;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        _taskRepo.Update(task);
        _taskRepo.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteTaskAsync(int id, int userId)
    {
        var task = _taskRepo.GetById(id);
        if (task == null || task.Project.UserId != userId) throw new UnauthorizedAccessException();

        _taskRepo.Delete(id);
        _taskRepo.SaveChanges();
        return Task.CompletedTask;
    }
}
