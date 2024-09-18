using Security.Domain.Users;

namespace Security.Application.Abstractions.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
