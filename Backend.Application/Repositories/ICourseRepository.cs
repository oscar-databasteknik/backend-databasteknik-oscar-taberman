using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface ICourseRepository
{
    Task AddCourseAsync(Course course);
    Task<Course> GetCourseByIdAsync(Guid courseId);
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Guid courseId);
}
