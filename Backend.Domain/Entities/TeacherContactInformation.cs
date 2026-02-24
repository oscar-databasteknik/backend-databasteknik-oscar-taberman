namespace Backend.Domain.Entities;

public sealed class TeacherContactInformation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}