using Pipes.Abstractions.Commands;

namespace Pipes.Command;

public sealed class CommandPipelineBuilder<TRequest> :
    ICommandPipelineBuilder<TRequest> where TRequest : ICommand
{
    private readonly List<Type> _pipeTypes = new();
    
    public void Use<TCommandPipe>() where TCommandPipe : ICommandPipe<TRequest>
    {
        _pipeTypes.Add(typeof(TCommandPipe));
    }

    public Type[] GetPipeTypes() => _pipeTypes.ToArray();
}