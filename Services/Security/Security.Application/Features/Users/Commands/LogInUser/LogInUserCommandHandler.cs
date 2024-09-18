using Security.Application.Abstractions.Authentication;
using Security.Application.Core.Messaging;
using Security.Domain.Abstractions;
using Security.Domain.Services;
using Security.Domain.Users;

namespace Security.Application.Features.Users.Commands.LogInUser;
internal class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public LogInUserCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<AccessTokenResponse>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidEmailOrPassword);

        bool passwordValid = user.IsCorrectPasswordHash(request.Password, _passwordHasher);

        if (!passwordValid)
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidEmailOrPassword);

        var result = _jwtTokenGenerator.GenerateToken(user);

        return new AccessTokenResponse(result);
    }
}
