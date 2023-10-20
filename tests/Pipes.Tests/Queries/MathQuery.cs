using Pipes.Abstractions.Query;

namespace Pipes.Tests.Queries;

public record MathQuery : IQuery<long>
{
    public long Number { get; init; }
}