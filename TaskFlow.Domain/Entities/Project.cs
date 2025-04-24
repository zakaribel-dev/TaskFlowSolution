namespace TaskFlow.Domain.Entities;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
