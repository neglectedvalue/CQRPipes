using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public sealed class CommandContext<TRequest>
{
    public TRequest  Request { get; set; }

    public CommandContext(TRequest request)
    {
        Request = request;
    }
}