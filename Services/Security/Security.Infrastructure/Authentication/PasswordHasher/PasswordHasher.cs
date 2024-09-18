
using System.Text.RegularExpressions;

using Security.Domain.Abstractions;
using Security.Domain.Services;

namespace Security.Infrastructure.Authentication.PasswordHasher;
public partial class PasswordHasher : IPasswordHasher
{
    private static readonly Regex PasswordRegex = StrongPasswordRegex();

    public Result<string> HashPassword(string password)
    {
        return !PasswordRegex.IsMatch(password)
            ? Result.Failure<string>(Error.PasswordTooWeak)
            : BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool IsCorrectPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }

    // https://stackoverflow.com/a/34715674/10091553
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();
}