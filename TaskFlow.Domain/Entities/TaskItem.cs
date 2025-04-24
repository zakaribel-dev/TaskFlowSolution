using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TaskItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
            public string Commentaires { get; set; } = ""; 
}
