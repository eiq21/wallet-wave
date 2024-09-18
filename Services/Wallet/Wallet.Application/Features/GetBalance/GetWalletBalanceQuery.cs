using Wallet.Application.Core.Messaging;

namespace Wallet.Application.Features.GetBalance;
public record GetWalletBalanceQuery(Guid WalletId) : IQuery<decimal>
{

}
