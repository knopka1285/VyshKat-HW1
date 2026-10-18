using System.Globalization;
using VyshKat.Domain.Things;
using VyshKat.Domain.Vehicles;
using VyshKat.Services;

namespace VyshKat;

/// <summary>
/// Консольный интерфейс «ВышКат»: весь ввод с клавиатуры и вывод на экран.
/// Бизнес-логика находится в классе Fleet, меню только вызывает её и показывает результат.
/// </summary>
public class ConsoleMenu
{
    /// <summary>Защита от повторной загрузки демо-данных.</summary>
    private bool _demoLoaded = false;
    
    private readonly Fleet _fleet;

    public ConsoleMenu(Fleet fleet)
    {
        _fleet = fleet;
    }

    /// <summary>Запускает меню и обрабатывает команды, пока пользователь не выберет выход.</summary>
    public void Show()
    {
        PrintMenu();
        while (true)
        {
            Console.Write("Введите номер действия (7 - меню): ");
            
            string? choice = Console.ReadLine();

            if (choice == "0")
            {
                return;
            }
            else if (choice == "1")
            {
                AcceptVehicle();
            }
            else if (choice == "2")
            {
                AddThing();
            }
            else if (choice == "3")
            {
                Console.WriteLine("\t \t Отчет");
                Console.WriteLine($"Количество предметов в парке: {_fleet.VehicleCount} единиц транспорта, " +
                                  $"{_fleet.ThingCount} единиц вещей");
                Console.WriteLine($"Всего единиц инвентаря в парке: {_fleet.VehicleCount + _fleet.ThingCount}");
                Console.WriteLine($"Потребляемая парком энергия: {_fleet.GetTotalDailyEnergyKwh()} кВт·ч в сутки");
            }
            else if (choice == "4")
            {
                Console.WriteLine("Весь транспорт для новичков в парке:");
                Console.WriteLine($"{"Название",-20} {"Тип"}");
                foreach (var vehicle in _fleet.GetVehiclesForBeginners())
                {
                    Console.WriteLine($"{vehicle.Name,-20} {vehicle.GetType().Name}");
                }
            }
            else if (choice == "5")
            {
                Console.WriteLine("Список инвентаря парка:");
                Console.WriteLine($"{"Название",-20} {"Инвентарный номер"}");
                foreach (var item in _fleet.GetInventory())
                {
                    Console.WriteLine($"{item.Name,-20} {item.InventoryNumber}");
                }
            }
            else if (choice == "6")
            {
                LoadDemoData();
            }
            else if (choice == "7")
            {
                Console.WriteLine();
                PrintMenu();
            }
            else
            {
                Console.WriteLine("К сожалению, такого действия нет в нашем меню.");
            }
            
            Console.WriteLine();
        }
    }

    /// <summary>Печатает целиком меню.</summary>
    private void PrintMenu()
    {
        Console.WriteLine("\t \t Меню ВышКат");
        Console.WriteLine("Чтобы выбрать действие введите его номер:");
        Console.WriteLine("0 - Выход");
        Console.WriteLine("1 - Принять транспорт");
        Console.WriteLine("2 - Добавить вещь");
        Console.WriteLine("3 - Отчет: количество предметов в парке и потребляемая энергия");
        Console.WriteLine("4 - Транспорт для новичков");
        Console.WriteLine("5 - Весь инвентарь: название и инвентарный номер");
        Console.WriteLine("6 - Загрузить демо-данные");
        Console.WriteLine("7 - Вывести меню");
        Console.WriteLine();
    }
    
    /// <summary>Читает целое число, переспрашивая до корректного ввода.</summary>
    private int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Нужно ввести целое число.");
        }
    }
    
    /// <summary>
    /// Читает дробное число, переспрашивая до корректного ввода.
    /// Принимает и запятую, и точку при любых региональных настройках.
    /// </summary>
    private double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (double.TryParse(input?.Replace(',', '.'), CultureInfo.InvariantCulture, out double number))
            {
                return number;
            }

            Console.WriteLine("Нужно ввести дробное число.");
        }
    }

    /// <summary>Читает непустую строку, переспрашивая до корректного ввода.</summary>
    private string ReadText(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            
            Console.WriteLine("Введите корректное имя - строка непустая и не состоит из пробелов");
        }
    }

    /// <summary>Читает инвентарный номер, переспрашивает пока не введут свободный номер в парке.</summary>
    private int ReadFreeInventoryNumber()
    {
        string outInvNum = "Введите инвентарный номер: ";
        int inventoryNum = ReadInt(outInvNum);
        
        while (_fleet.IsInventoryNumberTaken(inventoryNum))
        {
            Console.WriteLine("К сожалению, этот инвентарный номер занят. Выберите другой.");
            inventoryNum = ReadInt(outInvNum);
        }
        
        return inventoryNum;
    }

    /// <summary>Спрашивает данные транспорта и передаёт его в парк через техосмотр.</summary>
    private void AcceptVehicle()
    {
        Console.WriteLine("Введите код вида транспорта, который хотите добавить: ");
        Console.WriteLine("0 - Возврат в меню");
        Console.WriteLine("1 - Электросамокат");
        Console.WriteLine("2 - Велосипед");
        Console.WriteLine("3 - Электровелосипед");

        string message = "Вы добавляете транспорт вида: ";
        string outName = "Введите имя транспорта: ";
        string outEase = "Введите простоту транспорта в диапазоне от 1 до 10: ";
        string outEnergy = "Введите энергопотребление транспорта - дробное число: ";
        
        int vehicleType = ReadInt(message);
        
        while (vehicleType < 0 || vehicleType > 3)
        {
            Console.WriteLine("Такого вида транспорта нет в нашем парке. Введите 0, 1, 2 или 3.");
            vehicleType = ReadInt(message);
        }
        
        if (vehicleType == 0)
        {
            return;
        }
        
        string name = ReadText(outName);
        int inventoryNumber = ReadFreeInventoryNumber();
        int ease = ReadInt(outEase);

        Vehicle vehicle;
        if (vehicleType == 1)
        {
            double dailyEnergyKwh = ReadDouble(outEnergy);
            vehicle = new Scooter(name, inventoryNumber, ease, dailyEnergyKwh);
        }
        else if (vehicleType == 2)
        {
            vehicle = new Bicycle(name, inventoryNumber, ease);
        }
        else
        {
            double dailyEnergyKwh = ReadDouble(outEnergy);
            vehicle = new ElectricBike(name, inventoryNumber, ease, dailyEnergyKwh);
        }
        
        AcceptAndReport(vehicle);
    }

    /// <summary>Пытается принять транспорт в парк и печатает результат.</summary>
    private void AcceptAndReport(Vehicle vehicle)
    {
        if (_fleet.TryAccept(vehicle))
        {
            Console.WriteLine($"Транспорт «{vehicle.Name}» успешно добавлен в парк ВышКата!");
        }
        else
        {
            Console.WriteLine($"Ваш транспорт «{vehicle.Name}» не прошел технический осмотр.");
        }
    }

    /// <summary>Спрашивает данные вещи и ставит её на баланс.</summary>
    private void AddThing()
    {
        Console.WriteLine("Введите номер вещи, которую хотите добавить: ");
        Console.WriteLine("0 - Возврат в меню");        
        Console.WriteLine("1 - Шлем");
        Console.WriteLine("2 - Зарядный шкаф");
        Console.WriteLine("3 - Док-станция");
        
        string message = "Вы добавляете вещь вида: ";
        string outName = "Введите имя вещи: ";
        string outEnergy = "Введите энергопотребление вещи - дробное число: ";
        
        int thingType = ReadInt(message);
        while (thingType < 0 || thingType > 3)
        {
            Console.WriteLine("Такого вида вещи нет в нашем парке. Введите 1, 2 или 3, 0 - для выхода.");
            thingType = ReadInt(message);
        }

        if (thingType == 0)
        {
            return;
        }
        
        string name = ReadText(outName);
        int inventoryNumber = ReadFreeInventoryNumber();

        Thing thing;
        if (thingType == 1)
        {
            thing = new Helmet(name, inventoryNumber);
        }
        else if (thingType == 2)
        {
            double dailyEnergyKwh = ReadDouble(outEnergy);
            thing = new ChargingCabinet(name, inventoryNumber, dailyEnergyKwh);
        }
        else
        {
            double dailyEnergyKwh = ReadDouble(outEnergy);
            thing = new DockStation(name, inventoryNumber, dailyEnergyKwh);
        }
        
        _fleet.AddThing(thing);
        Console.WriteLine($"Вещь «{name}» добавлена в инвентарь ВышКата.");
    }

    /// <summary>
    /// Пункт 6: добавляет набор демо-данных, показывающий приём, отказ техосмотра,
    /// транспорт для новичков и вещи с расходом энергии и без. Повторно не загружается.
    /// </summary>
    private void LoadDemoData()
    {
        if (_demoLoaded)
        {
            Console.WriteLine("Демо-данные уже загружены.");
            return;
        }
        
        var scooter1 = new Scooter("Pro Scooter", 9001, 3, 15.0);
        AcceptAndReport(scooter1);
        
        var scooter2 = new Scooter("Usual Scooter", 9002, 10, 10.0);
        AcceptAndReport(scooter2);
        
        var scooter3 = new Scooter("Bad Scooter", 9007, 200, 150);
        AcceptAndReport(scooter3);
        
        var bicycle1 = new Bicycle("Cool Bicycle", 9052, 7);
        AcceptAndReport(bicycle1);
        
        var bicycle2 = new Bicycle("High Pro Bicycle", 9053, 0);
        AcceptAndReport(bicycle2);
        
        var electricBike = new ElectricBike("Best Electricbike", 9054, 6, 9.37);
        AcceptAndReport(electricBike);

        var helmet = new Helmet("Helmet Version01", 9060);
        _fleet.AddThing(helmet);
        Console.WriteLine($"Вещь «{helmet.Name}» добавлена в инвентарь ВышКата.");
        
        var dockStation = new DockStation("DockStation 13X8", 9065, 31.4);
        _fleet.AddThing(dockStation);
        Console.WriteLine($"Вещь «{dockStation.Name}» добавлена в инвентарь ВышКата.");
        
        var chargingCabinet = new ChargingCabinet("Charging Cabinet Low", 9067, 15.0);
        _fleet.AddThing(chargingCabinet);
        Console.WriteLine($"Вещь «{chargingCabinet.Name}» добавлена в инвентарь ВышКата.");
        
        _demoLoaded = true;
    }
}