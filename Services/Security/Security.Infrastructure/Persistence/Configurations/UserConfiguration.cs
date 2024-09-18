using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Domain.Users;

namespace Security.Infrastructure.Persistence.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "Security");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
           .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(user => user.Id)
        .ValueGeneratedNever()
        .HasColumnName("UserId");

        builder.Property(user => user.Email)
        .IsRequired()
        .HasMaxLength(126);

        builder.Property<string>("_passwordHash")
        .HasField("_passwordHash")
        .HasColumnName("PasswordHash")
        .IsRequired();
    }
}
