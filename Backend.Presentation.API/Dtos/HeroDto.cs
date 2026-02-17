namespace Backend.Presentation.API.Dtos;

public sealed record HeroDto
(
    string Title,
    string Header,
    string Description,
    string? ImageUrl
);
