using Wallet.Application.Core.Messaging;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Wallets;


namespace Wallet.Application.Features.Deposit;
internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DepositMoneyCommandHandler(
        IWalletRepository walletRepository,
        IUnitOfWork unitOfWork
        )
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DepositMoneyCommand request,
        CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByIdAsync(
            new WalletId(request.WalletId),
            cancellationToken);

        if (wallet is null)
            return Result.Failure(WalletErrors.NotFound);

        wallet.Deposit(request.Amount);

        _walletRepository.Update(wallet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
