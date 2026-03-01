namespace Backend.Domain.Entities;

public sealed class StudentGrade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string GradeValue { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
}
