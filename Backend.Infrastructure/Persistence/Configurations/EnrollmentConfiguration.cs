using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EnrollmentDate)
               .IsRequired();

        builder.HasOne(x => x.Student)
               .WithMany(x => x.Enrollments)
               .HasForeignKey(x => x.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CourseSession)
               .WithMany(x => x.Enrollments)
               .HasForeignKey(x => x.CourseSessionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}