using System.Net;
using System.Net.Http.Json;
using Backend.Presentation.API.Dtos;

namespace Backend.Tests.Integration.CourseSessions;

public sealed class CourseSessionApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CourseSessionApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Invalid_EndDate_Returns_BadRequest()
    {
        // Arrange - create a request for a temporary course session with an end date before the start date
        var request = new CreateCourseSessionRequest(
            CourseId: Guid.NewGuid(),
            ClassroomId: Guid.NewGuid(),
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddHours(-1),
            MaxParticipants: 10
        );

        // Act - post the course session
        var response = await _client.PostAsJsonAsync("/api/sessions", request, TestContext.Current.CancellationToken);

        // Assert - check the response is a BadRequest because invalid end date
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}