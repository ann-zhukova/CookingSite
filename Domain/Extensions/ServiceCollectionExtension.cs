using System.Reflection;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services.AddInIoc(Assembly.GetExecutingAssembly());
    }
}