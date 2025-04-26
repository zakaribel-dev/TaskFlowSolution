using TaskFlow.Domain.Entities;
using TaskFlow.WebApi.DTOs;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasksAsync(int userId);
    Task<TaskItem> GetTaskByIdAsync(int id, int userId);
    Task<TaskItem> CreateTaskAsync(TaskCreateDto dto, int userId);
    Task UpdateTaskAsync(int id, TaskUpdateDto dto, int userId);
    Task DeleteTaskAsync(int id, int userId);
}
