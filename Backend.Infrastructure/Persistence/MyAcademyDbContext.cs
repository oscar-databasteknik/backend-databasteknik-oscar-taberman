using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public sealed class MyAcademyDbContext(DbContextOptions<MyAcademyDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();

    public DbSet<CourseSession> CourseSessions => Set<CourseSession>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<CourseTeacher> CourseTeachers => Set<CourseTeacher>();

    public DbSet<StudentContactInformation> StudentContactInformations => Set<StudentContactInformation>();
    public DbSet<TeacherContactInformation> TeacherContactInformations => Set<TeacherContactInformation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Calls the base method to ensure any configurations from the base class (DbContext) are applied
        base.OnModelCreating(modelBuilder);

        // Register all IEntityTypeConfiguration classes  in this project
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAcademyDbContext).Assembly);
    }
}