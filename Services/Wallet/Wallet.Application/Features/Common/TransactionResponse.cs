namespace Wallet.Application.Features.Common;
public sealed class TransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string Type { get; set; }
    public Guid? RelatedWalletId { get; set; }
}
