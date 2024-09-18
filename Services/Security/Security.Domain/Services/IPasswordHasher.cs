using Security.Domain.Abstractions;

namespace Security.Domain.Services;
public interface IPasswordHasher
{
    public Result<string> HashPassword(string password);
    bool IsCorrectPassword(string password, string hash);
}
