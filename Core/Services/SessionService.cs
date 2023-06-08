using Contracts;
using Contracts.Models;
using Data.Repositories;
using System.Collections.Generic;

namespace Core.Services;
public class SessionService : ISessionService
{
    private readonly Dictionary<string, object> _sessionData = new Dictionary<string, object>();
    private readonly IPlayerService _playerService;
    private readonly ILobbyService _lobbyService;
    private readonly ICommandService _commandService;
    private readonly IMapService _mapService;
    public SessionService(IPlayerService playerService, ILobbyService lobbyService, ICommandService commandService)
    {
        _playerService = playerService;
        _lobbyService = lobbyService;
        _commandService = commandService;
        _mapService = new MapService(new UnitOfWork());
        SessionPlayer = null;
        PlayerTelegramId = 0;
    }
    public string LastInput 
    {
        get => (string)_sessionData.GetValueOrDefault("lastinput");
        set => _sessionData["lastinput"] = value;
    }
    public long UserTelegramId { get; set; }
    public Player SessionPlayer
    {
        get => (Player)_sessionData.GetValueOrDefault("player");
        set => _sessionData["player"] = value;
    }
    public long PlayerTelegramId
    {
        get => (long)_sessionData.GetValueOrDefault("player_telegram_id");
        set => _sessionData["player_telegram_id"] = value;
    }
    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(IPlayerService))
        {
            return _playerService;
        }
        else if (serviceType == typeof(IMapService))
        {
            return _mapService;
        }
        else if (serviceType == typeof(ILobbyService)) 
        {
            return _lobbyService;
        }
        else if (serviceType == typeof(ICommandService))
        {
            return _commandService;
        }
        return null;
    }
    public object GetData(string key)
    {
        return _sessionData.GetValueOrDefault(key);
    }
    public void SetData(string key, object value)
    {
        _sessionData[key] = value;
    }
}
