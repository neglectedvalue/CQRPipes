namespace Pipes.Abstractions.Query;

public interface IQueryHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken token);
}