namespace Backend.Domain.Entities;

public sealed class CourseCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;

    public List<Course> Courses { get; set; } = [];
}