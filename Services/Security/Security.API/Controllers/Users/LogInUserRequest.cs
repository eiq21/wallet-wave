namespace Security.API.Controllers.Users;
public sealed record LogInUserRequest(
    string Email,
    string Password
);
