using Microsoft.EntityFrameworkCore;
using Security.Domain.Users;

namespace Security.Infrastructure.Persistence.Repositories;
internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(SecurityDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
        .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}