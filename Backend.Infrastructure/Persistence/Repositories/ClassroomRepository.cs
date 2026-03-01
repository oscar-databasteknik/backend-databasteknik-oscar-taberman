using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class ClassroomRepository(MyAcademyDbContext database) : IClassroomRepository
{
    public async Task AddClassroomAsync(Classroom classroom, CancellationToken ct)
    {
        await database.Classrooms.AddAsync(classroom, ct);
        await database.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync(CancellationToken ct)
    {
        return await database.Classrooms
            .AsNoTracking()
            .Include(c => c.Sessions)
            .ToListAsync(ct);
    }

    public async Task<Classroom?> GetClassroomByIdAsync(Guid classroomId, CancellationToken ct)
    {
        if (classroomId == Guid.Empty)
            throw new ArgumentException("Classroom Id cannot be empty.", nameof(classroomId));

        var classroom = await database.Classrooms
            .Include(c => c.Sessions)
            .SingleOrDefaultAsync(c => c.Id == classroomId, ct)
            ?? throw new KeyNotFoundException($"Classroom Id {classroomId} not found.");

        return classroom;
    }

    public async Task UpdateClassroomAsync(Classroom classroom, CancellationToken ct)
    {
        var classroomUpdate = await database.Classrooms
            .SingleOrDefaultAsync(c => c.Id == classroom.Id, ct)
            ?? throw new KeyNotFoundException($"Classroom Id {classroom.Id} not found.");

        classroomUpdate.Name = classroom.Name;
        classroomUpdate.Capacity = classroom.Capacity;

        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteClassroomAsync(Guid classroomId, CancellationToken ct)
    {
        if (classroomId == Guid.Empty)
            throw new ArgumentException("Classroom Id cannot be empty.", nameof(classroomId));

        var classroom = await database.Classrooms
            .SingleOrDefaultAsync(c => c.Id == classroomId, ct)
            ?? throw new KeyNotFoundException($"Classroom Id {classroomId} not found.");

        database.Classrooms.Remove(classroom);
        await database.SaveChangesAsync(ct);
    }
}