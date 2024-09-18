using MediatR;
using Security.Domain.Abstractions;

namespace Security.Application.Core.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
