using Wallet.Application.Core.Messaging;

namespace Wallet.Application.Features.Deposit;
public sealed record DepositMoneyCommand(
    Guid WalletId,
    decimal Amount
) : ICommand;

