using Microsoft.Extensions.DependencyInjection;

namespace Core.IoC;

/// <summary>
///     Настройки добавления зависимости в контейнер
/// </summary>
public class PutInIoCAttribute : Attribute
{
    /// <summary>
    ///     Добавлять ли зависимость под своим собственным типа (иначе под интерфейсами)
    /// </summary>
    public bool Directly { get; set; }

    /// <summary>
    ///     Срок жизнь зависимости
    /// </summary>
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
}