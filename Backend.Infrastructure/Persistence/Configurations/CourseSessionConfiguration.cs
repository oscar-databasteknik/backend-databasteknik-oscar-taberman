using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CourseSessionConfiguration : IEntityTypeConfiguration<CourseSession>
{
    public void Configure(EntityTypeBuilder<CourseSession> builder)
    {
        builder.ToTable("CourseSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartDate)
               .IsRequired();

        builder.Property(x => x.EndDate)
               .IsRequired();

        builder.Property(x => x.MaxParticipants)
               .IsRequired();

        builder.HasOne(x => x.Course)
               .WithMany(x => x.Sessions)
               .HasForeignKey(x => x.CourseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.ClassroomId)
               .IsRequired();

        builder.HasOne(x => x.Classroom)
               .WithMany(c => c.Sessions)
               .HasForeignKey(x => x.ClassroomId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}