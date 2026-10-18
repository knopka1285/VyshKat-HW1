namespace VyshKat.Domain.Things;

public class ChargingCabinet : Thing, IEnergyConsumer
{
    public double  DailyEnergyKwh { get; }
    
    public ChargingCabinet(string name, int inventoryNumber,  double dailyEnergyKwh)
        : base(name, inventoryNumber)
    {
        DailyEnergyKwh = dailyEnergyKwh;
    }
}