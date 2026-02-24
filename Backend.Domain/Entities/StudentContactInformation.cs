namespace Backend.Domain.Entities;

public sealed class StudentContactInformation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}