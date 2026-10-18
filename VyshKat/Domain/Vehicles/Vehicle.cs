namespace VyshKat.Domain.Vehicles;

/// <summary>
/// Общий родитель для всех видов транспорта.
/// Энергопотребление здесь не вынесено, так как у некоторого транспорта его нет.
/// </summary>
public abstract class Vehicle : IInventory
{
    public string Name { get; }
    public int InventoryNumber { get; }

    /// <summary>Простота для новичка, от 1 до 10.</summary>
    public int Ease { get; }

    protected Vehicle(string name, int inventoryNumber, int ease)
    {
        Name = name;
        InventoryNumber = inventoryNumber;
        Ease = ease;
    }
}