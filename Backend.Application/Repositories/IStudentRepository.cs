using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student);
    Task<Student?> GetStudentByIdAsync(Guid studentId);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(Guid studentId);
}
