using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

/// <summary>
/// Query context
/// </summary>
/// <typeparam name="TRequest">Type of request</typeparam>
/// <typeparam name="TResponse">Type of response</typeparam>
[PublicAPI]
public class QueryContext<TRequest, TResponse>
{
    public TRequest  Request { get; set; }
    public TResponse Response { get; set; }
    
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="response">Response</param>
    public QueryContext(TRequest request, TResponse response = default!)
    {
        Request = request;
        Response = response;
    }
}