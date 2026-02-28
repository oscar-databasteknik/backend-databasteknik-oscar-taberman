using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student, CancellationToken ct);
    Task<Student?> GetStudentByIdAsync(Guid studentId, CancellationToken ct);
    Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken ct);
    Task UpdateStudentAsync(Student student, CancellationToken ct);
    Task DeleteStudentAsync(Guid studentId, CancellationToken ct);
}