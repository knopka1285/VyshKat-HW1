using VyshKat.Domain;
using VyshKat.Domain.Things;
using VyshKat.Domain.Vehicles;

namespace VyshKat.Tests;

public class VehicleTests
{
    [Fact]
    public void Scooter_Constructor_SetsAllProperties()
    {
        // Arrange & Act
        var scooter = new Scooter("Scooter V10X", 101, 7, 0.5);

        // Assert
        Assert.Equal("Scooter V10X", scooter.Name);
        Assert.Equal(101, scooter.InventoryNumber);
        Assert.Equal(7, scooter.Ease);
        Assert.Equal(0.5, scooter.DailyEnergyKwh);
    }

    [Fact]
    public void Bicycle_IsNotEnergyConsumer()
    {
        // Arrange & Act
        var bicycle = new Bicycle("Cool Bicycle", 102, 8);

        // Assert
        Assert.IsNotType<IEnergyConsumer>(bicycle, exactMatch: false);
    }

    [Fact]
    public void ChargingCabinet_IsEnergyConsumerAndInventory()
    {
        // Arrange & Act
        var cabinet = new ChargingCabinet("Cabinet", 103, 2.0);

        // Assert
        Assert.IsAssignableFrom<IEnergyConsumer>(cabinet);
        Assert.IsAssignableFrom<IInventory>(cabinet);
    }
}