using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Pipes.Abstractions.Query;
using Pipes.Query;

namespace Pipes.Tests;

public class QueryPipesTests
{
    
    [Fact]
    public void PipelineBuilder_GetPipeTypes_ShouldReturnCorrectLenght()
    {
        var pipelineBuilder = new QueryPipelineBuilder<SimpleQuery, Guid>();
        var simpleQueryPipeline = new SimpleQueryPipeline();
       
        simpleQueryPipeline.Configure(pipelineBuilder);

        pipelineBuilder.GetPipeTypes().Length.Should().Be(1);
    }

    [Fact]
    public async Task QueryDispatcher_Execute_ShouldNotFail()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddQueryPipes(ServiceLifetime.Transient, GetType().Assembly);
        
        var dispatcher = new PipesDispatcher(serviceCollection.BuildServiceProvider());
        var simpleQuery = new SimpleQuery();

        await dispatcher.Execute<SimpleQuery, Guid>(simpleQuery, CancellationToken.None);
    }
}

public class SimpleQuery : IQuery<Guid>
{
    public string Title { get; init; } = "UT";
}

public class SimpleQueryPipe : IQueryPipe<SimpleQuery, Guid>
{
    public async Task HandleAsync(QueryContext<SimpleQuery, Guid> context, Func<QueryContext<SimpleQuery, Guid>, Task> next, CancellationToken token = default)
    {
        context.Response = Guid.NewGuid();
        await next(context);
    }
}

public class SimpleQueryPipeline : IQueryPipeline<SimpleQuery, Guid>
{
    public void Configure(IQueryPipelineBuilder<SimpleQuery, Guid> builder)
    {
        builder.Use<SimpleQueryPipe>();
    }
}