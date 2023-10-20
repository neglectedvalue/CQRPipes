using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public sealed class CommandContext<TRequest, TResponse>
{
    public TRequest  Request { get; set; }
    public TResponse Response { get; set; }

    public CommandContext(TRequest request, TResponse response)
    {
        Request = request;
        Response = response;
    }
}