using Wallet.Application.Core.Messaging;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Wallets;


namespace Wallet.Application.Features.Transfer;
internal sealed class TransferMoneyCommandHandler : ICommandHandler<TransferMoneyCommand>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TransferMoneyCommandHandler(
        IWalletRepository walletRepository,
        IUnitOfWork unitOfWork
        )
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        TransferMoneyCommand request,
        CancellationToken cancellationToken)
    {
        var senderWallet = await _walletRepository.GetByIdAsync(
            new WalletId(request.SenderWalletId),
            cancellationToken);

        if (senderWallet == null)
            return Result.Failure(WalletErrors.WalletSenderNotFound);

        var recipientWallet = await _walletRepository.GetByIdAsync(
             new WalletId(request.RecipientWalletId),
            cancellationToken);

        if (recipientWallet == null)
            return Result.Failure(WalletErrors.WalletRecipientNotFound);

        senderWallet.Withdraw(request.Amount);

        _walletRepository.Update(senderWallet);

        _walletRepository.Update(recipientWallet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
