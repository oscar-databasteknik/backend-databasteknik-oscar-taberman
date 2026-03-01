namespace Backend.Presentation.API.Dtos;

public sealed record CourseResponse
(
    Guid Id,
    string Title,
    string Description,
    int Duration
);

public sealed record CreateCourseRequest
(
    string Title,
    string Description,
    int Duration
);

public sealed record UpdateCourseRequest
(
    string Title,
    string Description,
    int Duration
);