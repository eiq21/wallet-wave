using MediatR;
using Wallet.Domain.Abstractions;

namespace Wallet.Application.Core.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
