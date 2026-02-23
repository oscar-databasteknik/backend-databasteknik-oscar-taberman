using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public sealed class MyAcademyDbContext(DbContextOptions<MyAcademyDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");

            entity.HasKey(e => e.Id)
                  .HasName("Courses_Id_PK");

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(x => x.Description)
                  .IsRequired()
                  .HasMaxLength(2000);
                

            entity.Property(x => x.Duration)
                  .IsRequired();
        });
    }
}
