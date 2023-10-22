using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandHandler<in TRequest> where TRequest : ICommand
{
    Task HandleAsync(TRequest request, CancellationToken token);
}