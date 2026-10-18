using VyshKat.Domain.Vehicles;

namespace VyshKat.Services;

/// <summary>
/// Техосмотр: принимает транспорт с непустым названием и простотой от 1 до 10.
/// Проверяет бизнес-правила независимо от того, откуда пришли данные.
/// </summary>
public class ServiceCenter : IServiceCenter
{
    public bool Inspect(Vehicle vehicle)
    {
        return !string.IsNullOrWhiteSpace(vehicle.Name) && 1 <= vehicle.Ease && vehicle.Ease <= 10;
    }
}