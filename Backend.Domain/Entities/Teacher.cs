namespace Backend.Domain.Entities;

public sealed class Teacher
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string PersonalNumber { get; set; } = null!;

    // many <-> many
    public List<CourseTeacher> CourseTeachers { get; set; } = [];

    // 1 -> 1
    public TeacherContactInformation? ContactInformation { get; set; }
}