using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;

namespace Backend.Presentation.API.Mappings;

public static class ClassroomMapping
{
    public static ClassroomResponse ToClassroomResponse(this Classroom classroom)
    {
        return new ClassroomResponse(
            classroom.Id,
            classroom.Name,
            classroom.Capacity
        );
    }

    public static Classroom ToClassroom(this CreateClassroomRequest request)
    {
        return new Classroom
        {
            Name = request.Name,
            Capacity = request.Capacity
        };
    }

    public static void UpdateClassroom(this Classroom classroom, UpdateClassroomRequest request)
    {
        classroom.Name = request.Name;
        classroom.Capacity = request.Capacity;
    }
}