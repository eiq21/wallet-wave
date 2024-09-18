using Security.Domain.Abstractions;

namespace Security.Domain.Users;
public static class UserErrors
{
    public static Error NotFound = new(
           "User.Found",
           "The user with the specified identifier was not found");
    public static Error DuplicateEmail => new("User.DuplicateEmail", "The specified email is already in use.");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");

    public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");
}
