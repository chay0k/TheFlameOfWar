using Contracts;
using Contracts.Models;
using Core.Commands;
using Core.Services;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class NewGame : ICommand
{
    private readonly ILobbyService _lobbyService;
    private readonly ICommandService _commandService;
    private readonly IPlayerService _playerService;
    private readonly IServiceProvider _serviceProvider;

    public NewGame(IServiceProvider serviceProvider)
    {
        _lobbyService = serviceProvider.GetRequiredService<ILobbyService>();
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
        _serviceProvider = serviceProvider;

    }
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        var player = session.SessionPlayer;

        if (player == null)
        {
            _commandService.PushCommand(this, session.LastInput);
            _commandService.PushCommand(new Name(_serviceProvider), "");
            _commandService.ExpectedInput = true;
            return "Please enter your name before start";
        }

        var playerLobby = _lobbyService.GetByPlayer(player);
        var lobbyName = session.LastInput;

        switch (lobbyName)
        {
            case "":
                _commandService.PushCommand(this, "");
                _commandService.ExpectedInput = true;
                return "Please enter lobby name";
            case string name when playerLobby != null:
                _commandService.PushCommand(this, name);
                _commandService.PushCommand(new Sure(_serviceProvider), "");
                _commandService.ExpectedInput = true;
                return $"You take part in lobby \"{playerLobby.Name}\". \nTo create a new lobby, you will be excluded from the previous one. \nDo you agree with this?";
            default:
                var lobby = new Lobby
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = lobbyName,
                    Token = await TokenGenerator.GetRandomWord(lobbyName),
                    Map = null,
                    IsActive = true,
                    IsHotSeat = false,
                    Players = new List<Player>() { player },
                };
                _lobbyService.AddLobby(lobby);
                _commandService.ExpectedInput = false;
                return $"Lobby \"{lobbyName}\" token: \"{lobby.Token}\" created";
        }
    }
}
