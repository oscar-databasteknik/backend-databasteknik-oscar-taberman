using Backend.Domain.Entities;

namespace Backend.Application.Repositories;

public interface ITeacherRepository
{
        Task AddTeacherAsync(Teacher teacher);
        Task<Teacher?> GetTeacherByIdAsync(Guid teacherId);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(Guid teacherId);
}
