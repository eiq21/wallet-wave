namespace Security.API.Controllers.Users;
public sealed record RegisterUserRequest(
    string Email,
    string Password
);
