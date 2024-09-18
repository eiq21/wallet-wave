using Microsoft.EntityFrameworkCore;
using Wallet.Application.Exceptions;
using Wallet.Domain.Abstractions;

namespace Wallet.Infrastructure.Persistence;
public class WalletDbContext : DbContext, IUnitOfWork
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options)
    : base(options)
    { }

    public DbSet<Domain.Wallets.Wallet> Wallets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

}
