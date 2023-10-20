using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

[PublicAPI]
public interface IQueryHandler<in TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken token);
}