using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;
using Xunit;

namespace Backend.Tests.Unit.Courses;

public sealed class CourseMappingTests
{
    [Fact]
    public void ToCourseResponse_Returns_All_Values()
    {
        // Arrange - create temporary course
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = "C#",
            Description = "Basics",
            Duration = 10
        };

        // Act - send the course to the mapping method
        var response = course.ToCourseResponse();

        // Assert - check that the course values are the same as the response values
        Assert.Equal(course.Id, response.Id);
        Assert.Equal("C#", response.Title);
        Assert.Equal("Basics", response.Description);
        Assert.Equal(10, response.Duration);
    }

    [Fact]
    public void ToCourse_Creates_Course_From_CreateRequest()
    {
        // Arrange - create a request to create a temporary course
        var request = new CreateCourseRequest(
            Title: "ASP.NET",
            Description: "Minimal API",
            Duration: 5
        );

        // Act - send the request to the mapping method
        var course = request.ToCourse();

        // Assert - check that the course values are the same as the request values and Id is created
        Assert.Equal("ASP.NET", course.Title);
        Assert.Equal("Minimal API", course.Description);
        Assert.Equal(5, course.Duration);
        Assert.NotEqual(Guid.Empty, course.Id);
    }

    [Fact]
    public void UpdateCourse_Updates_Existing_Course()
    {
        // Arrange - create a temporary course and a request to update it
        var course = new Course
        {
            Title = "Old",
            Description = "Old desc",
            Duration = 1
        };

        var request = new UpdateCourseRequest(
            Title: "New",
            Description: "New desc",
            Duration: 99
        );

        // Act - send the update request to the mapping method
        course.UpdateCourse(request);

        // Assert - check that the course values are the same as the request values
        Assert.Equal("New", course.Title);
        Assert.Equal("New desc", course.Description);
        Assert.Equal(99, course.Duration);
    }
}