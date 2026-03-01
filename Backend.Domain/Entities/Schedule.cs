namespace Backend.Domain.Entities;

public sealed class Schedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Guid CourseSessionId { get; set; }
    public CourseSession CourseSession { get; set; } = null!;
}
