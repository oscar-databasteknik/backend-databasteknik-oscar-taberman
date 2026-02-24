namespace Backend.Domain.Entities;

public sealed class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PersonalNumber { get; set; }

    // many <-> many
    public List<Enrollment> Enrollments { get; set; } = [];

    // 1 -> 1
    public StudentContactInformation? ContactInformation { get; set; }
}