using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

[PublicAPI]
public interface IQueryPipelineBuilder<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    void Use<TQueryPipe>() where TQueryPipe : IQueryPipe<TRequest, TResponse>;
}