using Contracts;
using Contracts.Models;
using Contracts.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Commands;
public class NewGame : ICommand
{
    private readonly ILobbyService _lobbyService;
    private readonly ICommandService _commandService;
    private readonly ISessionService _sessionService;
    private readonly IServiceProvider _serviceProvider;

    public NewGame(IServiceProvider serviceProvider)
    {
        _lobbyService = serviceProvider.GetRequiredService<ILobbyService>();
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
        _sessionService = serviceProvider.GetRequiredService<ISessionService>();
        _serviceProvider = serviceProvider;

    }
    public async Task<string> ExecuteAsync()
    {
        var player = _sessionService.SessionPlayer;

        if (player == null)
        {
            _commandService.PushCommand(this, _sessionService.LastInput);
            _commandService.PushCommand(new Name(_serviceProvider), "");
            _commandService.ExpectedInput = true;
            return "Please enter your name before start";
        }

        var playerLobby = _lobbyService.GetByPlayer(player);
        var lobbyName = _sessionService.LastInput;

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
                _sessionService.Lobby = lobby;
                _commandService.ExpectedInput = false;
                return $"Lobby \"{lobbyName}\" token: \"{lobby.Token}\" created";
                
        }
    }
}
