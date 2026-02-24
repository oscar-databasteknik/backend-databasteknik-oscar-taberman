using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CourseTeacherConfiguration : IEntityTypeConfiguration<CourseTeacher>
{
    public void Configure(EntityTypeBuilder<CourseTeacher> builder)
    {
        builder.ToTable("CourseTeachers");

        builder.HasKey(x => new { x.CourseId, x.TeacherId });

        builder.HasOne(x => x.Course)
               .WithMany(x => x.CourseTeachers)
               .HasForeignKey(x => x.CourseId);

        builder.HasOne(x => x.Teacher)
               .WithMany(x => x.CourseTeachers)
               .HasForeignKey(x => x.TeacherId);
    }
}