namespace VyshKat.Domain.Vehicles;

public class ElectricBike : Vehicle, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public ElectricBike(string name, int inventoryNumber, int ease, double dailyEnergyKwh) 
        : base(name, inventoryNumber, ease)
    {
        DailyEnergyKwh = dailyEnergyKwh;
    }
}