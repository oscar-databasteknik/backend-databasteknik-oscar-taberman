namespace Backend.Domain.Entities;

public sealed class CourseTeacher
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}