using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class CourseRepository(MyAcademyDbContext database) : ICourseRepository
{


    public async Task AddCourseAsync(Course course, CancellationToken ct)
    {
        await database.AddAsync(course, ct);
        await database.SaveChangesAsync(ct);
    }
  

    public async Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken ct)
    {
        var courses = await database.Courses.AsNoTracking().ToListAsync(ct);

        return courses;
    }

    public async Task<Course?> GetCourseByIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentException("Course Id cannot be empty.", nameof(courseId));

        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId, ct)
            ?? throw new KeyNotFoundException($"Course Id {courseId} not found.");

        return course;
    }

    public async Task UpdateCourseAsync(Course courseId, CancellationToken ct)
    {
        if (courseId.Id == Guid.Empty)
            throw new ArgumentException("Course Id cannot be empty.", nameof(courseId.Id));

        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId.Id, ct)
            ?? throw new KeyNotFoundException($"Course Id {courseId.Id} not found.");

        course.Title = courseId.Title;
        course.Description = courseId.Description;
        course.Duration = courseId.Duration;

        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteCourseAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentException("Course Id cannot be empty.", nameof(courseId));

        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId, ct)
            ?? throw new KeyNotFoundException($"Course Id {courseId} not found.");

        database.Courses.Remove(course);
        await database.SaveChangesAsync(ct);
    }
}
