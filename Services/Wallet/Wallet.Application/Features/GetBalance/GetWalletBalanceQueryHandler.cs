using Wallet.Application.Core.Messaging;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Wallets;

namespace Wallet.Application.Features.GetBalance;
internal sealed class GetWalletBalanceQueryHandler : IQueryHandler<GetWalletBalanceQuery, decimal>
{
    private readonly IWalletRepository _walletRepository;

    public GetWalletBalanceQueryHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<Result<decimal>> Handle(GetWalletBalanceQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByIdAsync(new WalletId(request.WalletId), cancellationToken);

        if (wallet == null)
            return Result.Failure<decimal>(WalletErrors.NotFound);

        return Result.Success(wallet.Balance);
    }

}
