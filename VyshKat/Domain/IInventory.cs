namespace VyshKat.Domain;

/// <summary>
/// Объект, стоящий на балансе: транспорт или вещь.
/// </summary>
public interface IInventory
{
    /// <summary>Наименование.</summary>
    string Name { get; }

    /// <summary>Инвентарный номер, уникальный в пределах парка.</summary>
    int InventoryNumber { get; }
}