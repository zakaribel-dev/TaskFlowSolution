using TaskFlow.Domain.Enums;

namespace TaskFlow.WebApi.DTOs;

public class TaskUpdateDto
{
    public string? Title { get; set; }
    public TaskItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}
