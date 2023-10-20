using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public class SimpleQueryPipe : IQueryPipe<SimpleQuery, Guid>
{
    public async Task HandleAsync(QueryContext<SimpleQuery, Guid> context, Func<QueryContext<SimpleQuery, Guid>, Task> next, CancellationToken token = default)
    {
        context.Response = Guid.NewGuid();
        await next(context);
    }
}