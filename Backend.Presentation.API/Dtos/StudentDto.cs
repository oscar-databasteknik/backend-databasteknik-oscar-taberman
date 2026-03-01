namespace Backend.Presentation.API.Dtos;

public sealed record StudentResponse
(
    Guid Id,
    string Title,
    string Description,
    int Duration
);

public sealed record CreateStudentRequest
(
    string Title,
    string Description,
    int Duration
);

public sealed record UpdateStudentRequest
(
    string Title,
    string Description,
    int Duration
);