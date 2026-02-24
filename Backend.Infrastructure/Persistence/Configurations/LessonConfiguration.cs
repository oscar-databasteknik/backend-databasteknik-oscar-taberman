using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.LessonDate)
               .IsRequired();

        builder.HasOne(x => x.CourseSession)
               .WithMany(x => x.Lessons)
               .HasForeignKey(x => x.CourseSessionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}