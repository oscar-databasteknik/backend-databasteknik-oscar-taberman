using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;

namespace Backend.Presentation.API.Mappings;

public static class CourseSessionMapping
{
    public static CourseSessionResponse ToCourseSessionResponse(this CourseSession session)
    {
        return new CourseSessionResponse(
            session.Id,
            session.CourseId,
            session.ClassroomId,
            session.StartDate,
            session.EndDate,
            session.MaxParticipants
        );
    }

    public static CourseSession ToCourseSession(this CreateCourseSessionRequest request)
    {
        return new CourseSession
        {
            CourseId = request.CourseId,
            ClassroomId = request.ClassroomId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            MaxParticipants = request.MaxParticipants
        };
    }

    public static void UpdateCourseSession(this CourseSession session, UpdateCourseSessionRequest request)
    {
        session.CourseId = request.CourseId;
        session.ClassroomId = request.ClassroomId;
        session.StartDate = request.StartDate;
        session.EndDate = request.EndDate;
        session.MaxParticipants = request.MaxParticipants;
    }
}