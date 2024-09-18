using Security.Application.Core.Messaging;
using Security.Domain.Abstractions;
using Security.Domain.Users;
using Security.Domain.Services;

namespace Security.Application.Features.Users.Commands.RegisterUser;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (userExists is not null)
            return Result.Failure(UserErrors.DuplicateEmail);

        var passwordHashResult = _passwordHasher.HashPassword(request.Password);

        if (passwordHashResult.IsFailure)
            return Result.Failure(passwordHashResult.Error);

        var userResult = User.Create(request.Email, passwordHashResult.Value);

        _userRepository.Add(userResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
