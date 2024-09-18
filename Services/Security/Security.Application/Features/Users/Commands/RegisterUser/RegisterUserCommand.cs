using Security.Application.Core.Messaging;

namespace Security.Application.Features.Users.Commands.RegisterUser;
public sealed record RegisterUserCommand(
    string Email,
    string Password
) : ICommand;