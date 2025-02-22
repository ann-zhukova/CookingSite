using System.Reflection;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return services
            .AddDomain()
            .AddDbContext<PostgresContext, PostgresContext>(
                (provider, builder) =>
                {
                    builder.UseNpgsql(
                        //"Host=localhost;Port=5432;Database=cook;Username=postgres;Password=an124hy7"
                        connectionString
                    );
                }
            )
            .AddInIoc(Assembly.GetExecutingAssembly());
    }
}