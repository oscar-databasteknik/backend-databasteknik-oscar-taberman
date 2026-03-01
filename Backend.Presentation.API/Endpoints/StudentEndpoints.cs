using Backend.Application.Repositories;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;

namespace Backend.Presentation.API.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        app.MapGet("/api/students", GetAllStudentsAsync);
        app.MapGet("/api/students/{id:guid}", GetStudentByIdAsync);
        app.MapPost("/api/students", AddStudentAsync);
        app.MapPut("/api/students/{id:guid}", UpdateStudentAsync);
        app.MapDelete("/api/students/{id:guid}", DeleteStudentAsync);
    }

    // GET ALL
    private static async Task<IResult> GetAllStudentsAsync(IStudentRepository repo, CancellationToken ct)
    {
        var students = await repo.GetAllStudentsAsync(ct);
        var response = students.Select(s => s.ToStudentResponse()).ToList();
        return Results.Ok(response);
    }

    // GET
    private static async Task<IResult> GetStudentByIdAsync(Guid id, IStudentRepository repo, CancellationToken ct)
    {
        var student = await repo.GetStudentByIdAsync(id, ct);
        return student is null
            ? Results.NotFound()
            : Results.Ok(student.ToStudentResponse());
    }

    // POST
    private static async Task<IResult> AddStudentAsync(CreateStudentRequest request, IStudentRepository repo, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Results.BadRequest("FirstName is required");

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Results.BadRequest("LastName is required");

        if (string.IsNullOrWhiteSpace(request.PersonalNumber))
            return Results.BadRequest("PersonalNumber is required");

        var student = request.ToStudent();

        await repo.AddStudentAsync(student, ct);

        return Results.Created($"/api/students/{student.Id}", student.ToStudentResponse());
    }

    // PUT
    private static async Task<IResult> UpdateStudentAsync(Guid id,UpdateStudentRequest request, IStudentRepository repo, CancellationToken ct)
    {
        var student = await repo.GetStudentByIdAsync(id, ct);
        if (student is null)
            return Results.NotFound();

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Results.BadRequest("FirstName is required");

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Results.BadRequest("LastName is required");

        if (string.IsNullOrWhiteSpace(request.PersonalNumber))
            return Results.BadRequest("PersonalNumber is required");

        student.UpdateStudent(request);

        await repo.UpdateStudentAsync(student, ct);

        return Results.NoContent();
    }

    // DELETE
    private static async Task<IResult> DeleteStudentAsync(Guid id, IStudentRepository repo, CancellationToken ct)
    {
        var student = await repo.GetStudentByIdAsync(id, ct);
        if (student is null)
            return Results.NotFound();

        await repo.DeleteStudentAsync(id, ct);

        return Results.NoContent();
    }
}