using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;

namespace Backend.Presentation.API.Mappings;

public static class TeacherMapping
{
        public static TeacherResponse ToTeacherResponse(this Teacher teacher)
        {
            return new TeacherResponse(
                teacher.Id,
                teacher.FirstName,
                teacher.LastName,
                teacher.PersonalNumber
            );
        }
    
        public static Teacher ToTeacher(this CreateTeacherRequest request)
        {
            return new Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber
            };
        }
    
        public static void UpdateTeacher(this Teacher teacher, UpdateTeacherRequest request)
        {
            teacher.FirstName = request.FirstName;
            teacher.LastName = request.LastName;
            teacher.PersonalNumber = request.PersonalNumber;
    }
}
