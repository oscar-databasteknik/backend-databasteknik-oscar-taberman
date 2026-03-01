using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface IClassroomRepository
{
    Task AddClassroomAsync(Classroom classroom, CancellationToken ct);
    Task<Classroom?> GetClassroomByIdAsync(Guid classroomId, CancellationToken ct);
    Task<IEnumerable<Classroom>> GetAllClassroomsAsync(CancellationToken ct);
    Task UpdateClassroomAsync(Classroom classroom, CancellationToken ct);
    Task DeleteClassroomAsync(Guid classroomId, CancellationToken ct);
}