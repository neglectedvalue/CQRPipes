using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pipes.Abstractions;
using Pipes.Abstractions.Query;
using Scrutor;

namespace Pipes;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQueryPipes(this IServiceCollection serviceCollection,
        ServiceLifetime lifetime = ServiceLifetime.Scoped, 
        params Assembly[] assemblies)
    {
        if (assemblies.Any())
        {
            serviceCollection.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies).ApplyCqrPipesTypes(lifetime));
        }
        else
        {
            serviceCollection.Scan(typeSourceSelector => typeSourceSelector.FromEntryAssembly().ApplyCqrPipesTypes(lifetime));
        }

        serviceCollection.Add(new ServiceDescriptor(typeof(IPipesDispatcher), typeof(PipesDispatcher), lifetime));
        
        return serviceCollection;
    }

    private static void ApplyCqrPipesTypes(this IImplementationTypeSelector selector, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        selector
            // pipes
            .AddClasses(items => items.AssignableTo(typeof(IQueryPipe<,>)))
            .AsImplementedInterfaces()
            .AsSelf()
            .WithLifetime(lifetime)
            // pipelines
            .AddClasses(items => items.AssignableTo(typeof(IQueryPipeline<,>)))
            .AsImplementedInterfaces()
            .WithLifetime(lifetime)
            // query handlers
            .AddClasses(items => items.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithLifetime(lifetime);
    }
}