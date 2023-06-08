using Contracts;
using Contracts.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.LobbyCommands;
public class HotSeat : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        string message;
        var playerService = (IMapService)session.GetService(typeof(IMapService));
        var commandService = (ICommandService)session.GetService(typeof(ICommandService));
        var lobbyService = (ILobbyService)session.GetService(typeof(ILobbyService));

        var player = session.SessionPlayer;
        var name = session.LastInput;
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
