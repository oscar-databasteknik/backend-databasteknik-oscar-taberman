using Backend.Application.Repositories;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Persistence.Repositories;
using Backend.Presentation.API.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddDbContext<MyAcademyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyAcademyConnection")));
builder.Services.AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ITeacherRepository, TeacherRepository>()
                .AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddCors();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();


app.MapGet("/api/heroes", (HttpRequest request) =>
{
    var baseUrl = $"{request.Scheme}://{request.Host}";

    var heroes = new List<HeroDto>
    {
        new("myAcademy", "Välkommen till studentlivet", "Utveckla dina kunskaper inom programmering med oss online.", $"{baseUrl}/images/hero-university.jpg")
    };

    return Results.Ok(heroes);
});

app.Run();
