namespace Pipes.Abstractions.Query;

public interface IQueryPipeline<TRequest, TResponse> where TRequest: IQuery<TResponse>
{
    void Configure(IQueryPipelineBuilder<TRequest, TResponse> builder);
}