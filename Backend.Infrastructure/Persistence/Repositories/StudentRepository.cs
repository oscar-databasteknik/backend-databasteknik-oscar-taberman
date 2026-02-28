using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository(MyAcademyDbContext database) : IStudentRepository
{
    public async Task AddStudentAsync(Student student, CancellationToken ct)
    {
        await database.Students.AddAsync(student, ct);
        await database.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken ct)
    {
        return await database.Students
            .AsNoTracking()
            .Include(s => s.ContactInformation)
            .ToListAsync(ct);
    }

    public async Task<Student?> GetStudentByIdAsync(Guid studentId, CancellationToken ct)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student Id cannot be empty.", nameof(studentId));

        var student = await database.Students
            .Include(s => s.ContactInformation)
            .Include(s => s.Enrollments)
            .SingleOrDefaultAsync(s => s.Id == studentId, ct)
            ?? throw new KeyNotFoundException($"Student Id {studentId} not found.");

        return student;
    }

    public async Task UpdateStudentAsync(Student student, CancellationToken ct)
    {
        var studentUpdate = await database.Students
            .SingleOrDefaultAsync(s => s.Id == student.Id, ct)
            ?? throw new KeyNotFoundException($"Student Id {student.Id} not found.");

        studentUpdate.FirstName = student.FirstName;
        studentUpdate.LastName = student.LastName;
        studentUpdate.PersonalNumber = student.PersonalNumber;

        await database.SaveChangesAsync(ct);
    }

    public async Task DeleteStudentAsync(Guid studentId, CancellationToken ct)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student Id cannot be empty.", nameof(studentId));

        var student = await database.Students.SingleOrDefaultAsync(s => s.Id == studentId, ct)
            ?? throw new KeyNotFoundException($"Student Id {studentId} not found.");

        database.Students.Remove(student);
        await database.SaveChangesAsync(ct);
    }
}