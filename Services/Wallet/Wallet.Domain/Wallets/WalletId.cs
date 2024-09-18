namespace Wallet.Domain.Wallets;
public record WalletId(Guid Value)
{
    public static WalletId New() => new(Guid.NewGuid());
}
