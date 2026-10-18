using VyshKat.Domain.Vehicles;
using VyshKat.Services;

namespace VyshKat.Tests;

public class ServiceCenterTests
{
    [Fact]
    public void Inspect_ValidVehicle_ReturnsTrue()
    {
        // Arrange
        var serviceCenter = new ServiceCenter();
        var scooter = new Scooter("Scooter1", 12909, 4, 12.0);
        
        // Act
        var result = serviceCenter.Inspect(scooter);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Inspect_EaseBelowOne_ReturnsFalse()
    {
        // Arrange
        var serviceCenter = new ServiceCenter();
        var scooter = new Scooter("Scooter2", 12910, 0, 12.0);
        
        // Act
        var result = serviceCenter.Inspect(scooter);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Inspect_EaseAboveTen_ReturnsFalse()
    {
        // Arrange
        var serviceCenter = new ServiceCenter();
        var scooter = new Scooter("Scooter3", 12911, 11, 12.0);

        // Act
        var result = serviceCenter.Inspect(scooter);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Inspect_BlankName_ReturnsFalse()
    {
        // Arrange
        var serviceCenter = new ServiceCenter();
        var scooter = new Scooter("   ", 12912, 5, 12.0);

        // Act
        var result = serviceCenter.Inspect(scooter);

        // Assert
        Assert.False(result);
    }
}