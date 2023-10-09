using Pipes.Abstractions;
using Pipes.Abstractions.Query;
using Pipes.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Pipes;

public class PipesDispatcher : IPipesDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public PipesDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken token)
        where TRequest : IQuery<TResponse>
    {
        if (_serviceProvider.GetService(typeof(IQueryHandler<TRequest, TResponse>)) is
            IQueryHandler<TRequest, TResponse> queryHandler)
        {
            return await queryHandler.HandleAsync(request, token);
        }

        var context = new QueryContext<TRequest, TResponse>(request);
        var pipelineBuilder = new QueryPipelineBuilder<TRequest, TResponse>();
        var pipeline = _serviceProvider.GetRequiredService<IQueryPipeline<TRequest, TResponse>>() ?? throw new InvalidOperationException($"Pipeline does not exist for request '{request.GetType().FullName}'.");
        
        pipeline.Configure(pipelineBuilder);
        await ExecuteQueryPipelineAsync(pipelineBuilder.GetPipeTypes(), context, token);

        return context.Response;
    }

    private async Task ExecuteQueryPipelineAsync<TRequest, TResponse>(Type[] pipes,
        QueryContext<TRequest, TResponse> context,
        CancellationToken token)where TRequest : IQuery<TResponse>
    {
        if (pipes.Length is 0)
        {
            return;
        }

        var pipe = _serviceProvider.GetService(pipes.First()) as IQueryPipe<TRequest, TResponse> ?? throw new InvalidOperationException($"Pipe named '{pipes.First().FullName}' is not valid.");
        await pipe.HandleAsync(context, async ctx =>
        {
            if (pipes.Length > 1)
            {
                await ExecuteQueryPipelineAsync(pipes.Skip(1).ToArray(), ctx, token);
            }
        }, token);
    }
}