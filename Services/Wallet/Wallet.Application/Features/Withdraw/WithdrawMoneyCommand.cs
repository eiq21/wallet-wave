using Wallet.Application.Core.Messaging;

namespace Wallet.Application.Features.Deposit;
public sealed record WithdrawMoneyCommand(
    Guid WalletId,
    decimal Amount
) : ICommand;

