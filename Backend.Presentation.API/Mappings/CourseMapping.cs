using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;

namespace Backend.Presentation.API.Mappings;

public static class CourseMapping
{
    public static CourseResponse ToCourseResponse(this Course course)
    {
        return new CourseResponse(
            course.Id,
            course.Title,
            course.Description,
            course.Duration
        );
    }

    public static Course ToCourse(this CreateCourseRequest request)
    {
        return new Course
        {
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration
        };
    }

    public static void UpdateCourse(this Course course, UpdateCourseRequest request)
    {
        course.Title = request.Title;
        course.Description = request.Description;
        course.Duration = request.Duration;
    }
}
