using System.Collections.Immutable;
using Pipes.Abstractions.Query;

namespace Pipes.Query;

public sealed class QueryPipelineBuilder<TRequest, TResponse> :
    IQueryPipelineBuilder<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    private readonly List<Type> _pipeTypes = new();
    
    public void Use<TQueryPipe>() where TQueryPipe : IQueryPipe<TRequest, TResponse>
    {
        _pipeTypes.Add(typeof(TQueryPipe));
    }

    public ImmutableArray<Type> GetTypes() => _pipeTypes.ToImmutableArray();
}