using VyshKat.Domain;
using VyshKat.Domain.Vehicles;
using VyshKat.Domain.Things;
namespace VyshKat.Services;

/// <summary>
/// Парк «ВышКат» хранит транспорт и вещи на балансе и строит отчёты.
/// Транспорт попадает в парк только через техосмотр, вещи добавляются без проверки.
/// Ничего не печатает и не читает с консоли.
/// </summary>
public class Fleet
{
    /// <summary>Минимальная простота, при которой транспорт можно выдавать новичкам.</summary>
    private const int EaseForBeginners = 6;
    
    private readonly IServiceCenter _serviceCenter;
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();
    private readonly List<Thing> _things = new List<Thing>();
    
    /// <summary>Создаёт парк с заданным техосмотром.</summary>
    public Fleet(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter;
    }
    
    /// <summary>Отправляет транспорт на техосмотр и при успехе добавляет в парк. Возвращает true, если тррнспорт принят.</summary>
    public bool TryAccept(Vehicle vehicle)
    {
        if (!_serviceCenter.Inspect(vehicle))
        {
            return false;
        }
        
        _vehicles.Add(vehicle);
        return true;
    }

    /// <summary>Ставит вещь на баланс. Вещи техосмотр не проходят.</summary>
    public void AddThing(Thing thing)
    {
        _things.Add(thing);
    }

    /// <summary>Суммарный суточный расход энергии всех потребителей в парке, кВт·ч.</summary>
    public double GetTotalDailyEnergyKwh()
    {
        double dailyEnergyKwh = 0;
        foreach (var vehicle in _vehicles)
        {
            if (vehicle is IEnergyConsumer consumer)
            {
                dailyEnergyKwh += consumer.DailyEnergyKwh;
            }
        }

        foreach (var thing in _things)
        {
            if (thing is IEnergyConsumer consumer)
            {
                dailyEnergyKwh += consumer.DailyEnergyKwh;
            }
        }

        return dailyEnergyKwh;
    }

    /// <summary>Список транспорта, который можно выдавать новичкам.</summary>
    public List<Vehicle> GetVehiclesForBeginners()
    {
        List<Vehicle> approvedVehicles = new List<Vehicle>();

        foreach (var vehicle in _vehicles)
        {
            if (vehicle.Ease >= EaseForBeginners)
            {
                approvedVehicles.Add(vehicle);
            }
        }
        
        return approvedVehicles;
    }

    /// <summary>Всё, что стоит на балансе: транспорт и вещи в одном списке.</summary>
    public List<IInventory> GetInventory()
    {
        List<IInventory> allInventory = new List<IInventory>();
        
        foreach (var vehicle in _vehicles)
        {
            allInventory.Add(vehicle);
        }
        
        foreach (var thing in _things)
        {
            allInventory.Add(thing);
        }

        return allInventory;
    }

    /// <summary>Проверяет, занят ли инвентарный номер.</summary>
    public bool IsInventoryNumberTaken(int inventoryNumber)
    {
        List<IInventory> inventory = GetInventory();
        
        foreach (var invent in inventory)
        {
            if (invent.InventoryNumber == inventoryNumber)
            {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>Количество транспорта в парке.</summary>
    public int VehicleCount => _vehicles.Count;

    /// <summary>Количество вещей в парке.</summary>
    public int ThingCount => _things.Count;
}