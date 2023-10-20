using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public class MultiplyByTwoQueryPipe : IQueryPipe<MathQuery, long>
{
    public Task HandleAsync(QueryContext<MathQuery, long> context, Func<QueryContext<MathQuery, long>, Task> next, CancellationToken token = default)
    {
        context.Response = context.Request.Number * 2;

        return next(context);
    }
}