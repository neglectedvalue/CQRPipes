using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

/// <summary>
/// Pipeline builder, adds query pipe type to build whole processing pipeline.
/// Pipes will be called according to the First In First Called principle.
/// </summary>
/// <typeparam name="TRequest">Request type.</typeparam>
/// <typeparam name="TResponse">Response type.</typeparam>
[PublicAPI]
public interface IQueryPipelineBuilder<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    void Use<TQueryPipe>() where TQueryPipe : IQueryPipe<TRequest, TResponse>;
}