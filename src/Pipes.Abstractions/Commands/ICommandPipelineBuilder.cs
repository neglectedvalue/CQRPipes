namespace Pipes.Abstractions.Commands;

public interface ICommandPipelineBuilder<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    void Use<TCommandPipe>() where TCommandPipe : ICommandPipe<TRequest, TResponse>;
}