namespace VyshKat.Domain.Things;

public class DockStation : Thing, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }
    
    public DockStation(string name, int inventoryNumber, double dailyEnergyKwh)
        : base(name, inventoryNumber)
    {
        DailyEnergyKwh = dailyEnergyKwh;
    }
}