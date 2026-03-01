using Backend.Application.Repositories;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;

namespace Backend.Presentation.API.Endpoints;

public static class CourseSessionEndpoints
{
    public static void MapCourseSessionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/sessions", GetAllCourseSessionsAsync);
        app.MapGet("/api/sessions/{id:guid}", GetCourseSessionByIdAsync);
        app.MapPost("/api/sessions", AddCourseSessionAsync);
        app.MapPut("/api/sessions/{id:guid}", UpdateCourseSessionAsync);
        app.MapDelete("/api/sessions/{id:guid}", DeleteCourseSessionAsync);
    }

    private static async Task<IResult> GetAllCourseSessionsAsync(ICourseSessionRepository repo, CancellationToken ct)
    {
        var sessions = await repo.GetAllCourseSessionsAsync(ct);
        var response = sessions.Select(s => s.ToCourseSessionResponse()).ToList();
        return Results.Ok(response);
    }

    private static async Task<IResult> GetCourseSessionByIdAsync(Guid id, ICourseSessionRepository repo, CancellationToken ct)
    {
        var session = await repo.GetCourseSessionByIdAsync(id, ct);
        return session is null
            ? Results.NotFound()
            : Results.Ok(session.ToCourseSessionResponse());
    }

    private static async Task<IResult> AddCourseSessionAsync(CreateCourseSessionRequest request, ICourseSessionRepository repo, CancellationToken ct)
    {
        if (request.CourseId == Guid.Empty) return Results.BadRequest("CourseId is required.");
        if (request.ClassroomId == Guid.Empty) return Results.BadRequest("ClassroomId is required.");
        if (request.EndDate <= request.StartDate) return Results.BadRequest("EndDate must be after StartDate.");
        if (request.MaxParticipants <= 0) return Results.BadRequest("MaxParticipants must be greater than zero.");

        var session = request.ToCourseSession();

        await repo.AddCourseSessionAsync(session, ct);

        return Results.Created($"/api/sessions/{session.Id}", session.ToCourseSessionResponse());
    }

    private static async Task<IResult> UpdateCourseSessionAsync(Guid id, UpdateCourseSessionRequest request, ICourseSessionRepository repo, CancellationToken ct)
    {
        var session = await repo.GetCourseSessionByIdAsync(id, ct);
        if (session is null) return Results.NotFound();

        if (request.CourseId == Guid.Empty) return Results.BadRequest("CourseId is required.");
        if (request.ClassroomId == Guid.Empty) return Results.BadRequest("ClassroomId is required.");
        if (request.EndDate <= request.StartDate) return Results.BadRequest("EndDate must be after StartDate.");
        if (request.MaxParticipants <= 0) return Results.BadRequest("MaxParticipants must be greater than zero.");

        session.UpdateCourseSession(request);

        await repo.UpdateCourseSessionAsync(session, ct);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteCourseSessionAsync(Guid id, ICourseSessionRepository repo, CancellationToken ct)
    {
        var session = await repo.GetCourseSessionByIdAsync(id, ct);
        if (session is null) return Results.NotFound();

        await repo.DeleteCourseSessionAsync(id, ct);

        return Results.NoContent();
    }
}