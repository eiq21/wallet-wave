namespace Wallet.API.Controllers.Wallets;
public sealed record TransferRequest(
    Guid RecipientWalletId,
    decimal Amount
);
