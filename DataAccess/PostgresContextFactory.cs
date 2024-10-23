using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    [UsedImplicitly]
    internal sealed class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cook;Username=postgres;Password=an124hy7");
            return new(optionsBuilder.Options);
        }
    }
}
