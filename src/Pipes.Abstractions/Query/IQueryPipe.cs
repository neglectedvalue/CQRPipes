using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

/// <summary>
/// Basic query pipe component in querypipeline
/// </summary>
/// <typeparam name="TRequest">Request</typeparam>
/// <typeparam name="TResponse">Response</typeparam>
[PublicAPI]
public interface IQueryPipe<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    Task HandleAsync(QueryContext<TRequest, TResponse> context, Func<QueryContext<TRequest, TResponse>> next);
}