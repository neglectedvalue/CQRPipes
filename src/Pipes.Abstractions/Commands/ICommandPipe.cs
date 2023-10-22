using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandPipe<TRequest> where TRequest : ICommand
{
    Task HandleAsync(CommandContext<TRequest> context, Func<CommandContext<TRequest>, Task> next,
        CancellationToken token = default);
}