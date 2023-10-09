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
    /// <summary>
    /// Handle current query pipe
    /// </summary>
    /// <param name="context">Query context</param>
    /// <param name="next">Next pipe</param>
    /// <param name="token"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task HandleAsync(QueryContext<TRequest, TResponse> context, Func<QueryContext<TRequest, TResponse>, Task> next, CancellationToken token = default);
}