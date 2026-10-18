using VyshKat.Domain.Things;
using VyshKat.Domain.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using VyshKat;
using VyshKat.Services;

var services = new ServiceCollection();
services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<Fleet>();
services.AddSingleton<ConsoleMenu>();
var provider = services.BuildServiceProvider();
var fleet = provider.GetRequiredService<Fleet>();
var menu = provider.GetRequiredService<ConsoleMenu>();
menu.Show();
