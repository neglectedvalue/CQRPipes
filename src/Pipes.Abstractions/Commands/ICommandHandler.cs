using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandHandler<in TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken token);
}