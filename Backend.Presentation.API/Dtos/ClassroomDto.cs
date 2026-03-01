namespace Backend.Presentation.API.Dtos;

public sealed record ClassroomResponse(
    Guid Id,
    string Name,
    int Capacity
);

public sealed record CreateClassroomRequest(
    string Name,
    int Capacity
);

public sealed record UpdateClassroomRequest(
    string Name,
    int Capacity
);