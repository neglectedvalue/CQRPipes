using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public class SimpleQueryPipeline : IQueryPipeline<SimpleQuery, Guid>
{
    public void Configure(IQueryPipelineBuilder<SimpleQuery, Guid> builder)
    {
        builder.Use<SimpleQueryPipe>();
    }
}