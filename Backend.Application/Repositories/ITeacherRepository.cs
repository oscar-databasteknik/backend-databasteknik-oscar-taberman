using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface ITeacherRepository
{
    Task AddTeacherAsync(Teacher teacher, CancellationToken ct);
    Task<Teacher?> GetTeacherByIdAsync(Guid teacherId, CancellationToken ct);
    Task<IEnumerable<Teacher>> GetAllTeachersAsync(CancellationToken ct);
    Task UpdateTeacherAsync(Teacher teacher, CancellationToken ct);
    Task DeleteTeacherAsync(Guid teacherId, CancellationToken ct);
}