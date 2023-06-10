

using ConsoleClient;
using Contracts;
using Core;
using Core.Services;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<UnitOfWork>();

services.AddScoped<IPlayerRepository, PlayerRepository>();
services.AddScoped<IMapService, MapService>();
services.AddScoped<IPlayerService, PlayerService>();
services.AddScoped<ILobbyService, LobbyService>();
services.AddScoped<ISessionService, SessionService>();
services.AddScoped<ICommandService, CommandService>();
services.AddScoped<IResultPresenter, ConsoleResultPresenter>();

services.AddScoped<ConsoleEvents>();

var serviceProvider = services.BuildServiceProvider();

var session = serviceProvider.GetRequiredService<ISessionService>();
var consoleEvent = serviceProvider.GetRequiredService<ConsoleEvents>();

while (true)
{
    var cmd = Console.ReadLine();
    if (cmd.ToLower() ==  "/exit")
    { break; }
    session.LastInput = cmd;
    consoleEvent.ProceedAsync(cmd);
}