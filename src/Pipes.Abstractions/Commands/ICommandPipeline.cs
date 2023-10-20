using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandPipeline<TRequest, TResponse> where TRequest: ICommand<TResponse>
{
    void Configure(ICommandPipelineBuilder<TRequest, TResponse> builder);
}