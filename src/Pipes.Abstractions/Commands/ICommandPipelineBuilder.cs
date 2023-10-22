namespace Pipes.Abstractions.Commands;

public interface ICommandPipelineBuilder<TRequest> where TRequest : ICommand
{
    void Use<TCommandPipe>() where TCommandPipe : ICommandPipe<TRequest>;
}