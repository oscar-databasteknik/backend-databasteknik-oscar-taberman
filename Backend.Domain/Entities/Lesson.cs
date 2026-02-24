namespace Backend.Domain.Entities;

public sealed class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CourseSessionId { get; set; }
    public CourseSession CourseSession { get; set; } = null!;

    public string Title { get; set; } = null!;
    public DateTime LessonDate { get; set; }
}