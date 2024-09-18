using Security.Application.Core.Messaging;

namespace Security.Application.Features.Users.Commands.LogInUser;
public sealed record LogInUserCommand(string Email, string Password)
: ICommand<AccessTokenResponse>;
