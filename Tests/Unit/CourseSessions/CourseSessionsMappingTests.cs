using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;
using Xunit;

namespace Backend.Tests.Unit.CourseSessions;

public sealed class CourseSessionMappingTests
{
    [Fact]
    public void ToCourseSessionResponse_Returns_All_Values()
    {
        // Arrange -  create DateTime values and a coursesession instance
        var start = new DateTime(2026, 03, 10, 09, 00, 00, DateTimeKind.Utc);
        var end = new DateTime(2026, 03, 10, 12, 00, 00, DateTimeKind.Utc);

        var session = new CourseSession
        {
            Id = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            ClassroomId = Guid.NewGuid(),
            StartDate = start,
            EndDate = end,
            MaxParticipants = 20
        };

        // Act - send the request to mapping method
        var response = session.ToCourseSessionResponse();

        // Assert - check that course session response has the same values as course session
        Assert.Equal(session.Id, response.Id);
        Assert.Equal(session.CourseId, response.CourseId);
        Assert.Equal(session.ClassroomId, response.ClassroomId);
        Assert.Equal(session.StartDate, response.StartDate);
        Assert.Equal(session.EndDate, response.EndDate);
        Assert.Equal(session.MaxParticipants, response.MaxParticipants);
    }

    [Fact]
    public void ToCourseSession_Creates_CourseSession_From_CreateRequest()
    {
        // Arrange - create DateTime values and a create request instance
        var start = new DateTime(2026, 03, 15, 13, 30, 00, DateTimeKind.Utc);
        var end = new DateTime(2026, 03, 15, 16, 00, 00, DateTimeKind.Utc);

        var request = new CreateCourseSessionRequest(
            CourseId: Guid.NewGuid(),
            ClassroomId: Guid.NewGuid(),
            StartDate: start,
            EndDate: end,
            MaxParticipants: 12
        );

        // Act - send the request to mapping method
        var session = request.ToCourseSession();

        // Assert - check that course session has the same values as create request and Id is created
        Assert.Equal(request.CourseId, session.CourseId);
        Assert.Equal(request.ClassroomId, session.ClassroomId);
        Assert.Equal(request.StartDate, session.StartDate);
        Assert.Equal(request.EndDate, session.EndDate);
        Assert.Equal(request.MaxParticipants, session.MaxParticipants);
        Assert.NotEqual(Guid.Empty, session.Id);
    }

    [Fact]
    public void UpdateCourseSession_Updates_Existing_Session()
    {
        // Arrange - create a coursesession instance and an update request instance
        var originalId = Guid.NewGuid();

        var session = new CourseSession
        {
            Id = originalId,
            CourseId = Guid.NewGuid(),
            ClassroomId = Guid.NewGuid(),
            StartDate = new DateTime(2026, 03, 01, 08, 00, 00, DateTimeKind.Utc),
            EndDate = new DateTime(2026, 03, 01, 10, 00, 00, DateTimeKind.Utc),
            MaxParticipants = 5
        };

        var request = new UpdateCourseSessionRequest(
            CourseId: Guid.NewGuid(),
            ClassroomId: Guid.NewGuid(),
            StartDate: new DateTime(2026, 04, 01, 10, 00, 00, DateTimeKind.Utc),
            EndDate: new DateTime(2026, 04, 01, 12, 00, 00, DateTimeKind.Utc),
            MaxParticipants: 25
        );

        // Act - send the request to mapping method
        session.UpdateCourseSession(request);

        // Assert - check that course session has the same values as the update request and Id is unchanged
        Assert.Equal(originalId, session.Id);
        Assert.Equal(request.CourseId, session.CourseId);
        Assert.Equal(request.ClassroomId, session.ClassroomId);
        Assert.Equal(request.StartDate, session.StartDate);
        Assert.Equal(request.EndDate, session.EndDate);
        Assert.Equal(request.MaxParticipants, session.MaxParticipants);
    }
}