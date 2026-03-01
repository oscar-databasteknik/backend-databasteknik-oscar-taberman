namespace Backend.Presentation.API.Dtos;

public sealed record StudentResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string PersonalNumber
);

public sealed record CreateStudentRequest
(
    string FirstName,
    string LastName,
    string PersonalNumber
);

public sealed record UpdateStudentRequest
(
    string FirstName,
    string LastName,
    string PersonalNumber
);