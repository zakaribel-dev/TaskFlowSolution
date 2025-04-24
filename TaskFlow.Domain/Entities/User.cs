using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
