using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.PersonalNumber)
               .HasMaxLength(20);

        builder.HasOne(x => x.ContactInformation)
               .WithOne(x => x.Student)
               .HasForeignKey<StudentContactInformation>(x => x.StudentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}