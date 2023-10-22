using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Pipes.Query;
using Pipes.Tests.Queries;

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

        await dispatcher.ExecuteAsync<SimpleQuery, Guid>(simpleQuery, CancellationToken.None);
    }
    
    [Fact]
    public async Task QueryDispatcher_ExecuteMathQuery_ShouldNotFail()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddQueryPipes(ServiceLifetime.Transient, GetType().Assembly);
        
        var dispatcher = new PipesDispatcher(serviceCollection.BuildServiceProvider());
        var mathQuery = new MathQuery { Number = 2};

        var result = await dispatcher.ExecuteAsync<MathQuery, long>(mathQuery, CancellationToken.None);

        result.Should().Be(3);
    }
}