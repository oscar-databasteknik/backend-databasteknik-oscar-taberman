using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public sealed class MyAcademyDbContext(DbContextOptions<MyAcademyDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
