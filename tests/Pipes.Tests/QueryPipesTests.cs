using Microsoft.Extensions.DependencyInjection;
using Pipes.Abstractions.Query;

namespace Pipes.Tests;

public class QueryPipesTests
{
    [Fact]
    public void Dummy()
    {
        var serviceCollection = new ServiceCollection();
    }
}

public record SimpleQuery : IQuery<Guid>
{
    public string Title { get; init; } = "UT";
}

public class SimpleQueryPipe : IQueryPipe<SimpleQuery, Guid>
{
    public Task HandleAsync(QueryContext<SimpleQuery, Guid> context, Func<QueryContext<SimpleQuery, Guid>, Task> next, CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}

public class SimpleQueryPipeline : IQueryPipeline<SimpleQuery, Guid>
{
    public void Configure(IQueryPipelineBuilder<SimpleQuery, Guid> builder)
    {
        builder.Use<SimpleQueryPipe>();
    }
}