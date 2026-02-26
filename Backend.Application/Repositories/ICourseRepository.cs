using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface ICourseRepository
{
    Task AddCourseAsync(Course course, CancellationToken ct);
    Task<Course?> GetCourseByIdAsync(Guid courseId, CancellationToken ct);
    Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken ct);
    Task UpdateCourseAsync(Course course, CancellationToken ct);
    Task DeleteCourseAsync(Guid courseId, CancellationToken ct);
}
