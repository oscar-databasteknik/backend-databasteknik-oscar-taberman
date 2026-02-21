namespace Backend.Domain.Entities;

public sealed class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
}
