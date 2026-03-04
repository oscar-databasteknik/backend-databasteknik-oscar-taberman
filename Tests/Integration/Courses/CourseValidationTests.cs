using System.Net;
using System.Net.Http.Json;
using Backend.Presentation.API.Dtos;
using Xunit;

namespace Backend.Tests.Integration.Courses;

public sealed class CourseValidationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private const string BaseUrl = "/api/courses";
    private readonly HttpClient _client;

    public CourseValidationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<CourseResponse> CreateValidCourseAsync()
    {
        var create = new CreateCourseRequest(
            Title: "Valid course",
            Description: "Valid description",
            Duration: 5);

        var post = await _client.PostAsJsonAsync(BaseUrl, create, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.Created, post.StatusCode);

        var created = await post.Content.ReadFromJsonAsync<CourseResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(created);

        return created!;
    }


    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Post_Invalid_Title_Returns_BadRequest(string title)
    {
        var request = new CreateCourseRequest(
            Title: title,
            Description: "Desc",
            Duration: 10);

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task Post_Invalid_Duration_Returns_BadRequest(int duration)
    {
        var request = new CreateCourseRequest(
            Title: "Course",
            Description: "Desc",
            Duration: duration);

        var response = await _client.PostAsJsonAsync(BaseUrl, request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Put_Invalid_Title_Returns_BadRequest(string title)
    {
        var existing = await CreateValidCourseAsync();

        var update = new UpdateCourseRequest(
            Title: title,
            Description: "Updated",
            Duration: 10);

        var response = await _client.PutAsJsonAsync($"{BaseUrl}/{existing.Id}", update, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_Invalid_Duration_Returns_BadRequest(int duration)
    {
        var existing = await CreateValidCourseAsync();

        var update = new UpdateCourseRequest(
            Title: "Updated title",
            Description: "Updated",
            Duration: duration);

        var response = await _client.PutAsJsonAsync($"{BaseUrl}/{existing.Id}", update, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task Put_Unknown_Id_Returns_NotFound()
    {
        var id = Guid.NewGuid();

        var update = new UpdateCourseRequest(
            Title: "Does not matter",
            Description: "Does not matter",
            Duration: 10);

        var response = await _client.PutAsJsonAsync($"{BaseUrl}/{id}", update, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Unknown_Id_Returns_NotFound()
    {
        var id = Guid.NewGuid();

        var response = await _client.DeleteAsync($"{BaseUrl}/{id}", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}