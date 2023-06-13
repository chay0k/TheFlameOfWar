using ConsoleClient;
using Contracts;
using Contracts.Services;
using System.Configuration;
using Core.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<UnitOfWork>();

services.AddScoped<IRepository<MapEntity>>(provider => provider.GetService<UnitOfWork>().MapRepository);

services.AddScoped<IPlayerRepository, PlayerRepository>();
services.AddScoped<IPlayerService, PlayerService>();
services.AddScoped<ILandService, LandService>();
services.AddScoped<IResourceService, ResourceService>();
services.AddScoped<ICellService, CellService>();
services.AddScoped<IMapService, MapService>(); 

services.AddScoped<ILobbyService, LobbyService>();
services.AddScoped<ISessionService, SessionService>();
services.AddScoped<ICommandService, CommandService>();
services.AddScoped<IResultPresenter, ConsoleResultPresenter>();
services.AddDbContext<GameDbContext>(options =>
{
    options.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
});
services.AddScoped<ConsoleEvents>();

var serviceProvider = services.BuildServiceProvider();

var session = serviceProvider.GetRequiredService<ISessionService>();
var consoleEvent = serviceProvider.GetRequiredService<ConsoleEvents>();

var mapService = serviceProvider.GetRequiredService<IMapService>();
Console.WriteLine(mapService);
while (true)
{
    var cmd = Console.ReadLine();
    if (cmd.ToLower() == "/exit")
    {
        break;
    }
    session.LastInput = cmd;
    consoleEvent.ProceedAsync(cmd);
}
