namespace Wallet.Domain.Wallets;
public sealed class Wallet : Entity<WalletId>
{
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }

    public Wallet(WalletId id, Guid userId) : base(id)
    {
        UserId = userId;
        Balance = 0;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("El monto a depositar debe ser mayor a cero.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("El monto a retirar debe ser mayor a cero.");

        if (amount > Balance)
            throw new InvalidOperationException("Fondos insuficientes.");

        Balance -= amount;
    }

    public void Transfer(decimal amount, Wallet recipientWallet)
    {
        if (recipientWallet == null)
            throw new ArgumentNullException(nameof(recipientWallet));

        Withdraw(amount);
        recipientWallet.Deposit(amount);
    }
    private Wallet()
    {

    }

}
