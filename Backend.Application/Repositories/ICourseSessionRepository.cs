using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface ICourseSessionRepository
{
    Task AddCourseSessionAsync(CourseSession session, CancellationToken ct);
    Task<IEnumerable<CourseSession>> GetAllCourseSessionsAsync(CancellationToken ct);
    Task<CourseSession?> GetCourseSessionByIdAsync(Guid sessionId, CancellationToken ct);
    Task UpdateCourseSessionAsync(CourseSession session, CancellationToken ct);
    Task DeleteCourseSessionAsync(Guid sessionId, CancellationToken ct);
}