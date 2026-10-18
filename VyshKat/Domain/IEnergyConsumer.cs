namespace VyshKat.Domain;

/// <summary>
/// Устройство, потребляющее энергию: самокат, электровелосипед, зарядный шкаф, док-станция.
/// </summary>
public interface IEnergyConsumer
{
    /// <summary>Суточный расход энергии, кВт·ч.</summary>
    double DailyEnergyKwh { get; }
}