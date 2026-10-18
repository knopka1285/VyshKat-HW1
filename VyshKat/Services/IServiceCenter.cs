using VyshKat.Domain.Vehicles;

namespace VyshKat.Services;

/// <summary>
/// Техосмотр: решает, можно ли принять транспорт в активный парк.
/// </summary>
public interface IServiceCenter
{
    /// <summary>Проверяет транспорт.</summary>
    /// <returns><c>true</c>, если транспорт можно принять; иначе <c>false</c>.</returns>
    bool Inspect(Vehicle vehicle);
}