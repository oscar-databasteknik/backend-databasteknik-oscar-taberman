using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class CourseRepository(MyAcademyDbContext database, IMemoryCache cache) : ICourseRepository
{


    public async Task AddCourseAsync(Course course, CancellationToken ct)
    {
        await database.AddAsync(course, ct);
        await database.SaveChangesAsync(ct);

        cache.Remove("all_courses");
    }
  

    public async Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken ct)
    {
        const string cacheKey = "all_courses";

        if (cache.TryGetValue(cacheKey, out List<Course>? cachedCourses))
        {
            return cachedCourses!;
        }

        var courses = await database.Courses.AsNoTracking().ToListAsync(ct);

        cache.Set(cacheKey, courses, TimeSpan.FromMinutes(5));

        return courses;
    }

    public async Task<Course?> GetCourseByIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return null;

        var cacheKey = $"course_{courseId}";

        if (cache.TryGetValue(cacheKey, out Course? cachedCourse))
        {
            return cachedCourse;
        }

        var course = await database.Courses.AsNoTracking().SingleOrDefaultAsync(c => c.Id == courseId, ct);

        if (course is not null)
        {
            cache.Set(cacheKey, course, TimeSpan.FromMinutes(5));
        }

        return course;
    }

    public async Task UpdateCourseAsync(Course course, CancellationToken ct)
    {
        await database.SaveChangesAsync(ct);

        cache.Remove("all_courses");
        cache.Remove($"course_{course.Id}");
    }

    public async Task DeleteCourseAsync(Guid courseId, CancellationToken ct)
    {
        var course = await database.Courses.SingleOrDefaultAsync(c => c.Id == courseId, ct);
        if (course is null)
            return;

        database.Courses.Remove(course);
        await database.SaveChangesAsync(ct);

        cache.Remove("all_courses");
        cache.Remove($"course_{course.Id}");
    }

    public async Task<List<Course>> GetCoursesLongerThanDurationAsync(int minDuration, CancellationToken ct)
    {
        var cacheKey = $"courses_longer_than_{minDuration}";

        if (cache.TryGetValue(cacheKey, out List<Course>? cachedCourses))
        {
            return cachedCourses!;
        }

        var courses = await database.Courses.FromSqlInterpolated($"SELECT * FROM Courses WHERE Duration > {minDuration}").AsNoTracking().ToListAsync(ct);
        
        cache.Set(cacheKey, courses, TimeSpan.FromMinutes(5));

        return courses;
    }
}
