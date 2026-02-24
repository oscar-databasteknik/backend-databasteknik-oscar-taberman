namespace Backend.Domain.Entities;

public sealed class Enrollment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid CourseSessionId { get; set; }
    public CourseSession CourseSession { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
}