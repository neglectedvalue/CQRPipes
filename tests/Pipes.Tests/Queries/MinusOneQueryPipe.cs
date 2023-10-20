using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public class MinusOneQueryPipe : IQueryPipe<MathQuery, long>
{
    public Task HandleAsync(QueryContext<MathQuery, long> context, Func<QueryContext<MathQuery, long>, Task> next, CancellationToken token = default)
    {
        context.Response -= 1;

        return next(context);
    }
}