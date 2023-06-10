using Contracts;
using Contracts.Models;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.LobbyCommands;
public class HotSeat : ICommand
{
    private readonly IServiceProvider _serviceProvider;

    public HotSeat(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<string> ExecuteAsync(ISessionService session)
    {
        string message;

        var commandService = _serviceProvider.GetRequiredService<ICommandService>();
        var lobbyService = _serviceProvider.GetRequiredService<ILobbyService>();

        var player = session.SessionPlayer;
        var playerLobby = lobbyService.GetByPlayer(player);

        if (playerLobby == null)
        {
            message = $"you are not in any lobby yet \nUse commands {Constants.NewGame} or {Constants.Connect} to enter.";
            commandService.ExpectedInput = false;
        }
        else
        {
            playerLobby.ChangeGameMode();
            message = $"Hot seat {(playerLobby.IsHotSeat ? "enabled" : "disabled")}";
            commandService.ExpectedInput = false;
        }
        return message;
    }
}
