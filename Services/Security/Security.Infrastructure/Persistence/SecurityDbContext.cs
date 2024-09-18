using Microsoft.EntityFrameworkCore;
using Security.Application.Exceptions;
using Security.Domain.Abstractions;
using Security.Domain.Users;

namespace Security.Infrastructure.Persistence;
public class SecurityDbContext : DbContext, IUnitOfWork
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
    : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecurityDbContext).Assembly);
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
