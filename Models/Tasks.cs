namespace TaskManager.Models;

public class Tasks
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
    public required DateTime Duedate { get; set; }
}
