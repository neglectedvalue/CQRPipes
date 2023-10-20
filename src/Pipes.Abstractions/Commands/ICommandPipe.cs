using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandPipe<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    Task HandleAsync(CommandContext<TRequest, TResponse> context, Func<CommandContext<TRequest, TResponse>, Task> next,
        CancellationToken token = default);
}