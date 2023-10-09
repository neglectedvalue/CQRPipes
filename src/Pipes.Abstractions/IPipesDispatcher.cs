using Pipes.Abstractions.Query;

namespace Pipes.Abstractions;

public interface IPipesDispatcher
{
    Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken token) where TRequest : IQuery<TResponse>;
}