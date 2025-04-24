using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.DTOs;

public class TaskCreateDto
{
    public string? Title { get; set; }
    public TaskItemStatus Status { get; set; } 
    public int ProjectId { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Commentaires { get; set; }
}
