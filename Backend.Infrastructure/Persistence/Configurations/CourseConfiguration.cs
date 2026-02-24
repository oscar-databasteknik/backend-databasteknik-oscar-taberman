using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Persistence.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
              .IsRequired()
              .HasMaxLength(200);

        builder.Property(x => x.Description)
              .IsRequired()
              .HasMaxLength(2000);


        builder.Property(x => x.Duration)
              .IsRequired();
    }
}
