namespace TaskFlow.Domain.Entities;

public class Commentaire
{
    public int Id { get; set; }
    public string Texte { get; set; } = string.Empty;

    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } = null!;
}
