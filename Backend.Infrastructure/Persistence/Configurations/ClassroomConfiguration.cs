using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Persistence.Configurations;

public sealed class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.ToTable("Classrooms");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Capacity)
            .IsRequired();

        builder.HasMany(c => c.Sessions)
             .WithOne(s => s.Classroom)
             .HasForeignKey(s => s.ClassroomId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}