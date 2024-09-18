using MediatR;
using Wallet.Domain.Abstractions;

namespace Wallet.Application.Core.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}
