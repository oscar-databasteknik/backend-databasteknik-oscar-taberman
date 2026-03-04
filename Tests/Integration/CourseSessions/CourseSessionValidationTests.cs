using System.Net;
using System.Net.Http.Json;
using Backend.Presentation.API.Dtos;
using Xunit;

namespace Backend.Tests.Integration.CourseSessions;

public sealed class CourseSessionValidationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private const string BaseUrl = "/api/sessions";
    private readonly HttpClient _client;

    public CourseSessionValidationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private static CreateCourseSessionRequest ValidRequest() =>
        new(
            CourseId: Guid.NewGuid(),
            ClassroomId: Guid.NewGuid(),
            StartDate: DateTime.UtcNow.AddDays(1),
            EndDate: DateTime.UtcNow.AddDays(1).AddHours(2),
            MaxParticipants: 10
        );

    [Fact]
    public async Task Post_CourseId_Return_BadRequest_If_Empty()
    {
        var request = ValidRequest() with { CourseId = Guid.Empty };

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_ClassroomId_Return_BadRequest_If_Empty()
    {
        var request = ValidRequest() with { ClassroomId = Guid.Empty };

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_EndDate_Equals_StartDate_Returns_BadRequest()
    {
        var start = DateTime.UtcNow.AddDays(1);
        var request = ValidRequest() with { StartDate = start, EndDate = start };

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_EndDate_Before_StartDate_Returns_BadRequest()
    {
        var request = ValidRequest() with
        {
            StartDate = DateTime.UtcNow.AddDays(2),
            EndDate = DateTime.UtcNow.AddDays(1)
        };

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task Post_MaxParticipants_LessOrEqualZero_Returns_BadRequest(int maxParticipants)
    {
        var request = ValidRequest() with { MaxParticipants = maxParticipants };

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_Async_Returns_NotFound_If_Id_Is_Uknown()
    {
        var id = Guid.NewGuid();
        var update = new UpdateCourseSessionRequest(
            CourseId: Guid.NewGuid(),
            ClassroomId: Guid.NewGuid(),
            StartDate: DateTime.UtcNow.AddDays(1),
            EndDate: DateTime.UtcNow.AddDays(1).AddHours(1),
            MaxParticipants: 10
        );

        var response = await _client.PutAsJsonAsync($"{BaseUrl}/{id}", update, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Async_Returns_NotFound_If_Id_Is_Uknown()
    {
        var id = Guid.NewGuid();

        var response = await _client.DeleteAsync($"{BaseUrl}/{id}", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}