

using ConsoleClient;
using Core;
using Core.Servisces;
using Data.Repositories;

Console.WriteLine("Hello, World!");

var unitOfWork = new UnitOfWork();
var playerRep = new PlayerRepository(unitOfWork);
var session = new SessionService(new PlayerService(playerRep), new LobbyService());
var consoleEvent = new ConsoleEvents(session);
while (true)
{
    var cmd = Console.ReadLine();
    if (cmd.ToLower() ==  "/exit")
    { break; }
    session.LastInput = cmd;
    consoleEvent.ProceedAsync(cmd);
}