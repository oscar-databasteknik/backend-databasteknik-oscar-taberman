using Backend.Application.Repositories;
using Backend.Domain.Entities;
using Backend.Presentation.API.Dtos;
using Backend.Presentation.API.Mappings;

namespace Backend.Presentation.API.Endpoints;

public static class CourseEndpoints
{
        public static void MapCourseEndpoints(this WebApplication app)
        {
            app.MapGet("/api/courses", GetAllCoursesAsync);
            app.MapGet("/api/courses/{id:guid}", GetCourseByIdAsync);
            app.MapPost("/api/courses", AddCourseAsync);
            app.MapPut("/api/courses/{id:guid}", UpdateCourseAsync);
            app.MapDelete("/api/courses/{id:guid}", DeleteCourseAsync);
            app.MapGet("/api/courses/longer-than/{minDuration:int}", GetCoursesLongerThanDurationAsync);
    }

    //GET ALL
    private static async Task<IResult> GetAllCoursesAsync(ICourseRepository repo, CancellationToken ct)
        {
            var courses = await repo.GetAllCoursesAsync(ct);
            var response = courses.Select(c => c.ToCourseResponse()).ToList();
            return Results.Ok(response);
        }
        
        //GET
        private static async Task<IResult> GetCourseByIdAsync(Guid id, ICourseRepository repo, CancellationToken ct)
        {
            var course = await repo.GetCourseByIdAsync(id, ct);
            return course is null ? Results.NotFound() : Results.Ok(course.ToCourseResponse());
        }
        
        //POST
        private static async Task<IResult> AddCourseAsync(CreateCourseRequest request, ICourseRepository repo, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return Results.BadRequest("A course title is required");

            if (request.Duration <= 0)
                return Results.BadRequest("The course duration must be greater than zero");

            var course = request.ToCourse();

            await repo.AddCourseAsync(course, ct);
            return Results.Created($"/api/courses/{course.Id}", course.ToCourseResponse());
        }
        
        //PUT
        private static async Task<IResult> UpdateCourseAsync(Guid id, UpdateCourseRequest request, ICourseRepository repo, CancellationToken ct)
        {
            var course = await repo.GetCourseByIdAsync(id, ct);
            if (course is null)
                return Results.NotFound();

            if (string.IsNullOrWhiteSpace(request.Title))
                return Results.BadRequest("A course title is required");

            if (request.Duration <= 0)
                return Results.BadRequest("The course duration must be greater than zero");

            course.UpdateCourse(request);

            await repo.UpdateCourseAsync(course, ct);
            return Results.NoContent();
        }

        
        //DELETE
        private static async Task<IResult> DeleteCourseAsync(Guid id, ICourseRepository repo, CancellationToken ct)
        {
            var course = await repo.GetCourseByIdAsync(id, ct);
            if (course is null)
                return Results.NotFound();

            await repo.DeleteCourseAsync(id, ct);

            return Results.NoContent();
        }

        //GET
        private static async Task<IResult> GetCoursesLongerThanDurationAsync(int minDuration, ICourseRepository repo, CancellationToken ct)
        {
            if (minDuration <= 0)
                return Results.BadRequest("The minimum duration must be greater than zero");

            var courses = await repo.GetCoursesLongerThanDurationAsync(minDuration, ct);

            var response = courses.Select(c => c.ToCourseResponse()).ToList();

            return Results.Ok(response);
    }
}
