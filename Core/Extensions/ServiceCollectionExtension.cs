using System.Reflection;
using Core.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCore( this IServiceCollection services) =>
        services.AddInIoc(Assembly.GetExecutingAssembly());


    public static IServiceCollection AddInIoc<TLocator>(this IServiceCollection services) =>
        services.AddInIoc(typeof(TLocator).Assembly);
    
    public static IServiceCollection AddInIoc(this IServiceCollection services, Assembly assembly)
    {
        foreach (var implementation in assembly.GetTypes())
        {
            var configs = implementation.GetCustomAttribute<PutInIoCAttribute>();
            if (configs is null)
                continue;

            if (configs.Directly)
            {
                services.Add(new(implementation, implementation, configs.Lifetime));
                continue;
            }

            foreach (var service in implementation.GetInterfaces())
                services.Add(new(service, implementation, configs.Lifetime));
        }

        return services.AddAutoMapper(assembly);
    }
}