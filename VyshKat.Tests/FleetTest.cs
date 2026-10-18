using NSubstitute;
using VyshKat.Domain.Things;
using VyshKat.Domain.Vehicles;
using VyshKat.Services;

namespace VyshKat.Tests;

public class FleetTests
{
    [Fact]
    public void TryAccept_InspectionFailed_DoesNotAddVehicle()
    {
        // Arrange
        var serviceCenter = Substitute.For<IServiceCenter>();
        serviceCenter.Inspect(Arg.Any<Vehicle>()).Returns(false);
        var fleet = new Fleet(serviceCenter);
        var scooter = new Scooter("Scooter", 1, 7, 0.5);

        // Act
        var result = fleet.TryAccept(scooter);

        // Assert
        Assert.False(result);
        Assert.Equal(0, fleet.VehicleCount);
    }

    [Fact]
    public void TryAccept_InspectionPassed_AddsVehicle()
    {
        // Arrange
        var serviceCenter = Substitute.For<IServiceCenter>();
        serviceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);
        var fleet = new Fleet(serviceCenter);
        var scooter = new Scooter("Scooter", 1, 100, 0.5);

        // Act
        var result = fleet.TryAccept(scooter);

        // Assert
        Assert.True(result);
        Assert.Equal(1, fleet.VehicleCount);
    }
    
    [Fact]
    public void TryAccept_AnyVehicle_CallsInspectOnce()
    {
        // Arrange
        var serviceCenter = Substitute.For<IServiceCenter>();
        var fleet = new Fleet(serviceCenter);
        var scooter = new Scooter("Scooter", 1, 7, 0.5);

        // Act
        fleet.TryAccept(scooter);

        // Assert
        serviceCenter.Received(1).Inspect(scooter);
    }

    [Fact]
    public void GetTotalDailyEnergyKwh_MixedFleet_SumsOnlyConsumers()
    {
        // Arrange
        var fleet = CreateFleetAcceptingAll();
        fleet.TryAccept(new Scooter("Scooter", 1, 7, 0.5));
        fleet.TryAccept(new Bicycle("Bicycle", 2, 7));
        fleet.AddThing(new Helmet("Helmet", 3));
        fleet.AddThing(new ChargingCabinet("Cabinet", 4, 2.0));

        // Act
        var total = fleet.GetTotalDailyEnergyKwh();

        // Assert
        Assert.Equal(2.5, total, 3);
    }

    [Fact]
    public void GetVehiclesForBeginners_EaseSixAndFive_ReturnsOnlyEaseSix()
    {
        // Arrange
        var fleet = CreateFleetAcceptingAll();
        var easy = new Bicycle("Easy", 1, 6);
        var hard = new Bicycle("Hard", 2, 5);
        fleet.TryAccept(easy);
        fleet.TryAccept(hard);

        // Act
        var result = fleet.GetVehiclesForBeginners();

        // Assert
        Assert.Single(result);
        Assert.Contains(easy, result);
    }

    [Fact]
    public void GetInventory_VehiclesAndThings_ReturnsAll()
    {
        // Arrange
        var fleet = CreateFleetAcceptingAll();
        fleet.TryAccept(new Scooter("Scooter", 1, 7, 0.5));
        fleet.AddThing(new Helmet("Helmet", 2));

        // Act
        var inventory = fleet.GetInventory();

        // Assert
        Assert.Equal(2, inventory.Count);
    }

    [Fact]
    public void IsInventoryNumberTaken_ExistingNumber_ReturnsTrue()
    {
        // Arrange
        var fleet = CreateFleetAcceptingAll();
        fleet.AddThing(new Helmet("Helmet", 42));

        // Act
        var taken = fleet.IsInventoryNumberTaken(42);

        // Assert
        Assert.True(taken);
    }

    [Fact]
    public void IsInventoryNumberTaken_FreeNumber_ReturnsFalse()
    {
        // Arrange
        var fleet = CreateFleetAcceptingAll();
        fleet.AddThing(new Helmet("Helmet", 42));

        // Act
        var taken = fleet.IsInventoryNumberTaken(43);

        // Assert
        Assert.False(taken);
    }

    private static Fleet CreateFleetAcceptingAll()
    {
        var serviceCenter = Substitute.For<IServiceCenter>();
        serviceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);
        return new Fleet(serviceCenter);
    }
}