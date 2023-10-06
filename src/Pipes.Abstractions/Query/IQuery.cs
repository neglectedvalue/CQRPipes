using JetBrains.Annotations;

namespace Pipes.Abstractions.Query;

/// <summary>
/// Basic interface for queries
/// </summary>
/// <typeparam name="TResponse">Type of response</typeparam>
[PublicAPI]
public interface IQuery<TResponse> { }