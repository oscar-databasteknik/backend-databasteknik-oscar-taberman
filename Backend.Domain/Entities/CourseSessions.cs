namespace Backend.Domain.Entities;

public sealed class CourseSession
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int MaxParticipants { get; set; }

    // 1 -> many
    public List<Lesson> Lessons { get; set; } = [];

    // many <-> many
    public List<Enrollment> Enrollments { get; set; } = [];
    public Guid ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = null!;
}