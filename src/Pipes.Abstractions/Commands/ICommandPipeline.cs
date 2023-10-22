using JetBrains.Annotations;

namespace Pipes.Abstractions.Commands;

[PublicAPI]
public interface ICommandPipeline<TRequest> where TRequest: ICommand
{
    void Configure(ICommandPipelineBuilder<TRequest> builder);
}