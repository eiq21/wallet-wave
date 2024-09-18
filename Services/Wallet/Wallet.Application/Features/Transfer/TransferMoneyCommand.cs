using Wallet.Application.Core.Messaging;

namespace Wallet.Application.Features.Transfer;
public sealed record TransferMoneyCommand(
    Guid SenderWalletId,
    Guid RecipientWalletId,
    decimal Amount
) : ICommand;