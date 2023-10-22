using Pipes.Abstractions;
using Pipes.Abstractions.Query;
using Pipes.Query;
using Microsoft.Extensions.DependencyInjection;
using Pipes.Abstractions.Commands;
using Pipes.Command;

namespace Pipes;

public class PipesDispatcher : IPipesDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public PipesDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request, CancellationToken token)
        where TRequest : IQuery<TResponse>
    {
        if (_serviceProvider.GetService(typeof(IQueryHandler<TRequest, TResponse>)) is
            IQueryHandler<TRequest, TResponse> queryHandler)
        {
            return await queryHandler.HandleAsync(request, token);
        }

        var context = new QueryContext<TRequest, TResponse>(request);
        var pipelineBuilder = new QueryPipelineBuilder<TRequest, TResponse>();
        var pipeline = _serviceProvider.GetRequiredService<IQueryPipeline<TRequest, TResponse>>() ?? 
                       throw new InvalidOperationException($"Pipeline does not exist for request '{request.GetType().FullName}'.");
        
        pipeline.Configure(pipelineBuilder);
        await ExecuteQueryPipelineAsync(pipelineBuilder.GetPipeTypes(), context, token);

        return context.Response;
    }

    public async Task ExecuteAsync<TRequest>(TRequest request, CancellationToken token) where TRequest : ICommand
    {
        if (_serviceProvider.GetService(typeof(ICommandHandler<TRequest>)) is
            ICommandHandler<TRequest> commandHandler)
        {
            await commandHandler.HandleAsync(request, token);
        }

        var context = new CommandContext<TRequest>(request);
        var pipelineBuilder = new CommandPipelineBuilder<TRequest>();
        var pipeline = _serviceProvider.GetRequiredService<ICommandPipeline<TRequest>>() ?? 
                       throw new InvalidOperationException($"Pipeline does not exist for request '{request.GetType().FullName}'.");
        
        pipeline.Configure(pipelineBuilder);
        await ExecuteCommandPipelineAsync(pipelineBuilder.GetPipeTypes(), context, token);
    }

    private async Task ExecuteCommandPipelineAsync<TRequest>(Type[] pipes,
        CommandContext<TRequest> context,
        CancellationToken token) where TRequest : ICommand
    {
        if (pipes.Length is 0)
        {
            return;
        }

        var pipe = _serviceProvider.GetRequiredService(pipes.First()) as ICommandPipe<TRequest> ?? 
                   throw new InvalidOperationException($"Pipe named '{pipes.First().FullName}' is not valid.");
        await pipe.HandleAsync(context, async ctx =>
        {
            if (pipes.Length > 1)
            {
                await ExecuteCommandPipelineAsync(pipes.Skip(1).ToArray(), ctx, token);
            }
        }, token);
    }

    private async Task ExecuteQueryPipelineAsync<TRequest, TResponse>(Type[] pipes,
        QueryContext<TRequest, TResponse> context,
        CancellationToken token)where TRequest : IQuery<TResponse>
    {
        if (pipes.Length is 0)
        {
            return;
        }

        var pipe = _serviceProvider.GetRequiredService(pipes.First()) as IQueryPipe<TRequest, TResponse> ?? 
                   throw new InvalidOperationException($"Pipe named '{pipes.First().FullName}' is not valid.");
        await pipe.HandleAsync(context, async ctx =>
        {
            if (pipes.Length > 1)
            {
                await ExecuteQueryPipelineAsync(pipes.Skip(1).ToArray(), ctx, token);
            }
        }, token);
    }
}