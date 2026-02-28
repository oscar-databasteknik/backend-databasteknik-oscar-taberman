using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class TeacherRepository(MyAcademyDbContext database) : ITeacherRepository
{
    public async Task AddTeacherAsync(Teacher teacher, CancellationToken ct)
    {
        await database.Teachers.AddAsync(teacher, ct);
        await database.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync(CancellationToken ct)
    {
        return await database.Teachers
            .AsNoTracking()
            .Include(t => t.ContactInformation)
            .ToListAsync(ct);
    }

    public async Task<Teacher?> GetTeacherByIdAsync(Guid teacherId, CancellationToken ct)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher Id cannot be empty.", nameof(teacherId));

        var teacher = await database.Teachers
            .Include(t => t.ContactInformation)
            .Include(t => t.CourseTeachers)
            .SingleOrDefaultAsync(t => t.Id == teacherId, ct)
            ?? throw new KeyNotFoundException($"Teacher Id {teacherId} not found.");

        return teacher;
    }

    public async Task UpdateTeacherAsync(Teacher teacher, CancellationToken ct)
    {
        var teacherUpdate = await database.Teachers
            .SingleOrDefaultAsync(t => t.Id == teacher.Id, ct)
            ?? throw new KeyNotFoundException($"Teacher Id {teacher.Id} not found.");

        teacherUpdate.FirstName = teacher.FirstName;
        teacherUpdate.LastName = teacher.LastName;

        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteTeacherAsync(Guid teacherId, CancellationToken ct)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher Id cannot be empty.", nameof(teacherId));

        var teacher = await database.Teachers.SingleOrDefaultAsync(t => t.Id == teacherId, ct)
            ?? throw new KeyNotFoundException($"Teacher Id {teacherId} not found.");

        database.Teachers.Remove(teacher);
        await database.SaveChangesAsync(ct);
    }
}