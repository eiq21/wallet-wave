using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Wallets;

namespace Wallet.Infrastructure.Persistence.Repositories;
internal sealed class WalletRepository : Repository<Domain.Wallets.Wallet, WalletId>, IWalletRepository
{
    public WalletRepository(WalletDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Domain.Wallets.Wallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Wallets
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
    }
}
