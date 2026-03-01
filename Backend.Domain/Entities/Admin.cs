namespace Backend.Domain.Entities;

public sealed class Admin
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PersonalNumber { get; set; } = null!;

    // 1 -> 1
    public AdminContactInformation? ContactInformation { get; set; }
}