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
            return null;

        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId, ct);

        return course;
    }

    public async Task UpdateCourseAsync(Course course, CancellationToken ct)
    {
        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteCourseAsync(Guid courseId, CancellationToken ct)
    {
        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId, ct);
        if (course is null)
            return;

        database.Courses.Remove(course);
        await database.SaveChangesAsync(ct);
    }

    public async Task<List<Course>> GetCoursesLongerThanDurationAsync(int minDuration, CancellationToken ct)
    {
        var courses = await database.Courses.FromSqlInterpolated($"SELECT * FROM Courses WHERE Duration > {minDuration}").AsNoTracking().ToListAsync(ct);
        return courses;
    }
}
