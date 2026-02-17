using Backend.Presentation.API.Dtos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
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
        new("Course Online", "Välkommen till studentlivet", "Utveckla dina kunskaper med oss online.", $"{baseUrl}/images/hero-university.jpg")
    };

    return Results.Ok(heroes);
});

app.Run();
