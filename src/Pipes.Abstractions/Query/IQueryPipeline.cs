using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

[PublicAPI]
public interface IQueryPipeline<TRequest, TResponse> where TRequest: IQuery<TResponse>
{
    void Configure(IQueryPipelineBuilder<TRequest, TResponse> builder);
}