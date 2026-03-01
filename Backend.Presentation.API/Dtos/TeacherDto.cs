namespace Backend.Presentation.API.Dtos;

public sealed record TeacherResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string PersonalNumber
);

public sealed record CreateTeacherRequest
(
    string FirstName,
    string LastName,
    string PersonalNumber
);

public sealed record UpdateTeacherRequest
(
    string FirstName,
    string LastName,
    string PersonalNumber
);