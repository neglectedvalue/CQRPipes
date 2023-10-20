using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public class MathQueryPipeline : IQueryPipeline<MathQuery, long>
{
    public void Configure(IQueryPipelineBuilder<MathQuery, long> builder)
    {
        builder.Use<MultiplyByTwoQueryPipe>();
        builder.Use<MinusOneQueryPipe>();
    }
}