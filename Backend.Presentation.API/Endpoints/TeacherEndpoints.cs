using Backend.Application.Repositories;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;

namespace Backend.Presentation.API.Endpoints;

public static class TeacherEndpoints
{
    public static void MapTeacherEndpoints(this WebApplication app)
    {
        app.MapGet("/api/teachers", GetAllTeachersAsync);
        app.MapGet("/api/teachers/{id:guid}", GetTeacherByIdAsync);
        app.MapPost("/api/teachers", AddTeacherAsync);
        app.MapPut("/api/teachers/{id:guid}", UpdateTeacherAsync);
        app.MapDelete("/api/teachers/{id:guid}", DeleteTeacherAsync);
    }

    // GET ALL
    private static async Task<IResult> GetAllTeachersAsync( ITeacherRepository repo, CancellationToken ct)
    {
        var teachers = await repo.GetAllTeachersAsync(ct);
        var response = teachers.Select(t => t.ToTeacherResponse()).ToList();
        return Results.Ok(response);
    }

    // GET
    private static async Task<IResult> GetTeacherByIdAsync(Guid id, ITeacherRepository repo, CancellationToken ct)
    {
        var teacher = await repo.GetTeacherByIdAsync(id, ct);
        return teacher is null
            ? Results.NotFound()
            : Results.Ok(teacher.ToTeacherResponse());
    }

    // POST
    private static async Task<IResult> AddTeacherAsync(CreateTeacherRequest request, ITeacherRepository repo, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Results.BadRequest("FirstName is required");

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Results.BadRequest("LastName is required");

        if (string.IsNullOrWhiteSpace(request.PersonalNumber))
            return Results.BadRequest("PersonalNumber is required");

        var teacher = request.ToTeacher();

        await repo.AddTeacherAsync(teacher, ct);

        return Results.Created($"/api/teachers/{teacher.Id}", teacher.ToTeacherResponse());
    }

    // PUT
    private static async Task<IResult> UpdateTeacherAsync(Guid id, UpdateTeacherRequest request, ITeacherRepository repo, CancellationToken ct)
    {
        var teacher = await repo.GetTeacherByIdAsync(id, ct);
        if (teacher is null)
            return Results.NotFound();

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Results.BadRequest("FirstName is required");

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Results.BadRequest("LastName is required");

        if (string.IsNullOrWhiteSpace(request.PersonalNumber))
            return Results.BadRequest("PersonalNumber is required");

        teacher.UpdateTeacher(request);

        await repo.UpdateTeacherAsync(teacher, ct);

        return Results.NoContent();
    }

    // DELETE
    private static async Task<IResult> DeleteTeacherAsync( Guid id, ITeacherRepository repo, CancellationToken ct)
    {
        var teacher = await repo.GetTeacherByIdAsync(id, ct);
        if (teacher is null)
            return Results.NotFound();

        await repo.DeleteTeacherAsync(id, ct);

        return Results.NoContent();
    }
}