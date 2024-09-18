using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Infrastructure.Persistence.Configurations;
internal sealed class WalletConfiguration : IEntityTypeConfiguration<Domain.Wallets.Wallet>
{
    public void Configure(EntityTypeBuilder<Domain.Wallets.Wallet> builder)
    {
        builder.ToTable("Wallet", "Wallet");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasConversion(
                id => id.Value,
                value => new Domain.Wallets.WalletId(value));

        builder.Property(a => a.Id)
        .ValueGeneratedNever()
        .HasColumnName("WalletId");

        builder.Property(w => w.Balance)
            .HasColumnType("decimal(18,2)");
    }
}