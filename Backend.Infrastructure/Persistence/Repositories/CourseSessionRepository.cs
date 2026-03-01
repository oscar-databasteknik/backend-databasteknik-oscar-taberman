using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class CourseSessionRepository(MyAcademyDbContext database) : ICourseSessionRepository
{
    public async Task AddCourseSessionAsync(CourseSession session, CancellationToken ct)
    {
        await database.CourseSessions.AddAsync(session, ct);
        await database.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<CourseSession>> GetAllCourseSessionsAsync(CancellationToken ct)
    {
        return await database.CourseSessions
            .AsNoTracking()
            .Include(cs => cs.Course)
            .Include(cs => cs.Classroom)
            .ToListAsync(ct);
    }

    public async Task<CourseSession?> GetCourseSessionByIdAsync(Guid sessionId, CancellationToken ct)
    {
        if (sessionId == Guid.Empty)
            return null;

        return await database.CourseSessions
            .Include(cs => cs.Course)
            .Include(cs => cs.Classroom)
            .SingleOrDefaultAsync(cs => cs.Id == sessionId, ct);
    }

    public async Task UpdateCourseSessionAsync(CourseSession session, CancellationToken ct)
    {
        var existing = await database.CourseSessions.SingleOrDefaultAsync(cs => cs.Id == session.Id, ct);
        if (existing is null) return;

        existing.CourseId = session.CourseId;
        existing.ClassroomId = session.ClassroomId;
        existing.StartDate = session.StartDate;
        existing.EndDate = session.EndDate;
        existing.MaxParticipants = session.MaxParticipants;

        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteCourseSessionAsync(Guid sessionId, CancellationToken ct)
    {
        if (sessionId == Guid.Empty)
            return;

        var existing = await database.CourseSessions.SingleOrDefaultAsync(cs => cs.Id == sessionId, ct);
        if (existing is null) return;

        database.CourseSessions.Remove(existing);
        await database.SaveChangesAsync(ct);
    }
}