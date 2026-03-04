using System.Net;
using System.Net.Http.Json;
using Backend.Presentation.API.Dtos;
using Backend.Tests.Integration;

namespace Backend.Tests.Integration.Courses;

public sealed class CourseApiTest
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CourseApiTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Then_GetById_Works()
    {
        // Arrange - create a request to create a temporary course
        var create = new CreateCourseRequest(
            Title: "Integration",
            Description: "Test",
            Duration: 10);

        // Act - post the course
        var post = await _client.PostAsJsonAsync("/api/courses", create, TestContext.Current.CancellationToken);

        // Assert - check that the posted course is equal to the saved course
        Assert.Equal(HttpStatusCode.Created, post.StatusCode);
        // check that the course is not empty
        var created = await post.Content.ReadFromJsonAsync<CourseResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(created);
        // check that the course can be fetched with id
        var get = await _client.GetAsync($"/api/courses/{created!.Id}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);
    }
}