using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Wallet.Application.Core.Messaging;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Wallets;

namespace Wallet.Application.Features.CreateWallet;
internal sealed class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand, Guid>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateWalletCommandHandler(
        IWalletRepository walletRepository,
    IUnitOfWork unitOfWork)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        // var user = await _walletRepository.GetByUserIdAsync(
        //     request.UserId,
        //     cancellationToken);

        // if (user is null)
        //     return Result.Failure(WalletErrors.UserNotFound);

        var wallet = new Domain.Wallets.Wallet(new WalletId(Guid.NewGuid()), request.UserId);

        _walletRepository.Add(wallet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(wallet.Id.Value);
    }
}
