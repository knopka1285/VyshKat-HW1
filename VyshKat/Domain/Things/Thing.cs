namespace VyshKat.Domain.Things;

/// <summary>
/// Общий родитель для вещей на балансе: шлемов, док-станций, зарядных шкафов.
/// Вещи не проходят техосмотр и не имеют характеристики «простота».
/// </summary>
public abstract class Thing : IInventory
{
    public string Name { get; }
    public int InventoryNumber { get; }

    protected Thing(string name, int inventoryNumber)
    {
        Name = name;
        InventoryNumber = inventoryNumber;
    }
}