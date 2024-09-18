namespace Wallet.Domain.Wallets;
public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(WalletId id, CancellationToken cancellationToken = default);
    Task<Wallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Update(Wallet wallet);
    void Add(Wallet wallet);
}
