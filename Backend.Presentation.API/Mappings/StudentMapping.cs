using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;

namespace Backend.Presentation.API.Mappings;

public static class StudentMapping
{
    public static StudentResponse ToStudentResponse(this Student student)
    {
        return new StudentResponse(
            student.Id,
            student.FirstName,
            student.LastName,
            student.PersonalNumber
        );
    }

    public static Student ToStudent(this CreateStudentRequest request)
    {
        return new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PersonalNumber = request.PersonalNumber
        };
    }

    public static void UpdateStudent(this Student student, UpdateStudentRequest request)
    {
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.PersonalNumber = request.PersonalNumber;
    }
}
