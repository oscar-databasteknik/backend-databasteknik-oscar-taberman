namespace Backend.Presentation.API.Dtos;

public sealed record TeacherResponse
(
    Guid Id,
    string Title,
    string Description,
    int Duration
);

public sealed record CreateTeacherRequest
(
    string Title,
    string Description,
    int Duration
);

public sealed record UpdateTeacherRequest
(
    string Title,
    string Description,
    int Duration
);