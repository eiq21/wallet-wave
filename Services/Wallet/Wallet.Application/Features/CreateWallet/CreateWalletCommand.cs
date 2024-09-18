using Wallet.Application.Core.Messaging;

namespace Wallet.Application.Features.CreateWallet;
public sealed record CreateWalletCommand(
    Guid UserId
) : ICommand<Guid>;
