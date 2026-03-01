namespace Backend.Presentation.API.Dtos;

public sealed record CourseSessionResponse(
    Guid Id,
    Guid CourseId,
    Guid ClassroomId,
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants
);

public sealed record CreateCourseSessionRequest(
    Guid CourseId,
    Guid ClassroomId,
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants
);

public sealed record UpdateCourseSessionRequest(
    Guid CourseId,
    Guid ClassroomId,
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants
);