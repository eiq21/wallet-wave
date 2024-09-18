using MediatR;
using Security.Domain.Abstractions;

namespace Security.Application.Core.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}
