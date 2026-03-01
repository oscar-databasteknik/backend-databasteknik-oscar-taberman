using Backend.Application.Repositories;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;

namespace Backend.Presentation.API.Endpoints;

public static class ClassroomEndpoints
{
    public static void MapClassroomEndpoints(this WebApplication app)
    {
        app.MapGet("/api/classrooms", GetAllClassroomsAsync);
        app.MapGet("/api/classrooms/{id:guid}", GetClassroomByIdAsync);
        app.MapPost("/api/classrooms", AddClassroomAsync);
        app.MapPut("/api/classrooms/{id:guid}", UpdateClassroomAsync);
        app.MapDelete("/api/classrooms/{id:guid}", DeleteClassroomAsync);
    }

    // GET ALL
    private static async Task<IResult> GetAllClassroomsAsync(IClassroomRepository repo, CancellationToken ct)
    {
        var classrooms = await repo.GetAllClassroomsAsync(ct);
        var response = classrooms.Select(c => c.ToClassroomResponse()).ToList();
        return Results.Ok(response);
    }

    // GET
    private static async Task<IResult> GetClassroomByIdAsync(Guid id, IClassroomRepository repo, CancellationToken ct)
    {
        var classroom = await repo.GetClassroomByIdAsync(id, ct);
        return classroom is null
            ? Results.NotFound()
            : Results.Ok(classroom.ToClassroomResponse());
    }

    // POST
    private static async Task<IResult> AddClassroomAsync(CreateClassroomRequest request, IClassroomRepository repo, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("A classroom name is required");

        if (request.Capacity <= 0)
            return Results.BadRequest("Capacity must be greater than zero");

        var classroom = request.ToClassroom();

        await repo.AddClassroomAsync(classroom, ct);

        return Results.Created($"/api/classrooms/{classroom.Id}", classroom.ToClassroomResponse());
    }

    // PUT
    private static async Task<IResult> UpdateClassroomAsync(Guid id, UpdateClassroomRequest request, IClassroomRepository repo, CancellationToken ct)
    {
        var classroom = await repo.GetClassroomByIdAsync(id, ct);
        if (classroom is null)
            return Results.NotFound();

        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("A classroom name is required");

        if (request.Capacity <= 0)
            return Results.BadRequest("Capacity must be greater than zero");

        classroom.UpdateClassroom(request);

        await repo.UpdateClassroomAsync(classroom, ct);

        return Results.NoContent();
    }

    // DELETE
    private static async Task<IResult> DeleteClassroomAsync(Guid id, IClassroomRepository repo, CancellationToken ct)
    {
        var classroom = await repo.GetClassroomByIdAsync(id, ct);
        if (classroom is null)
            return Results.NotFound();

        await repo.DeleteClassroomAsync(id, ct);

        return Results.NoContent();
    }
}