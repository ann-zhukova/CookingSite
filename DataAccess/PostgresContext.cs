using DataAccess.Type;
using DataAccess.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

/// <summary>
///     cd DataAccess
///     dotnet ef migrations add "NAME" - создает новую миграцию с именем NAME, но не применяет ее к БД
///     dotnet ef database update - применяет все непримененные миграции к БД
///     dotnet ef database update "NAME" - накатывает/откатывает БД до состояния миграции NAME
///     dotnet ef migrations remove - удаляет последнюю миграцию из кода (важно! она должна быть непримененной к БД)
/// </summary>
internal sealed class PostgresContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TypeEntity> Types { get; set; }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<LevelEntity>()
        //     .HasMany(l => l.Figures)
        //     .WithMany(f => f.Levels)
        //     .UsingEntity(t => t.ToTable("LevelUseFigure"));
    }
}