using Pipes.Abstractions.Commands;
using Pipes.Abstractions.Query;

namespace Pipes.Abstractions;

public interface IPipesDispatcher
{
    Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request, CancellationToken token) where TRequest : IQuery<TResponse>;
    Task ExecuteAsync<TRequest>(TRequest request, CancellationToken token) where TRequest : ICommand;
}