using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class StudentContactInformationConfiguration
    : IEntityTypeConfiguration<StudentContactInformation>
{
    public void Configure(EntityTypeBuilder<StudentContactInformation> builder)
    {
        builder.ToTable("StudentContactInformations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
               .HasMaxLength(200);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(50);

        builder.Property(x => x.Address)
               .HasMaxLength(300);
    }
}