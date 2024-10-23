using System.Reflection;
using Core.Extensions;

namespace Front.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFront( this IServiceCollection services) =>
        services.AddInIoc(Assembly.GetExecutingAssembly());
}