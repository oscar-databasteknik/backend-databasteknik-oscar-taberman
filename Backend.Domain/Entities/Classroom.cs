namespace Backend.Domain.Entities;

public sealed class Classroom
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public List<CourseSession> Sessions { get; set; } = [];
}
