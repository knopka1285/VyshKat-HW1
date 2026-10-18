namespace VyshKat.Domain.Vehicles;

public class Scooter : Vehicle, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public Scooter(string name, int inventoryNumber, int ease, double dailyEnergyKwh)
        : base(name, inventoryNumber, ease)
    {
        DailyEnergyKwh = dailyEnergyKwh;
    }
}