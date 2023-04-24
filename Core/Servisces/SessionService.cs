using Contracts;
using Contracts.Models;
using System.Collections.Generic;

namespace Core.Servisces;
public class SessionService : ISessionService
{
    private readonly Dictionary<string, object> _sessionData = new Dictionary<string, object>();
    private readonly IPlayerService _playerService;
    private readonly ILobbyService _lobbyService;
    private Stack<ICommand> commands = new Stack<ICommand>();

    public ICommand PeekCommand()
    {
        if (commands.Count() == 0)
            return null;
        return (ICommand)commands.Peek();
    }
    public void PushCommand(ICommand command)
    {
        commands.Push(command);
    }
    public ICommand PopCommand()
    {
        if (commands.Count() == 0)
            return null;
        return commands.Pop();
    }
    public void ClearCommands()
    {
        commands.Clear();
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
    public SessionService(IPlayerService playerService, ILobbyService lobbyService)
    {
        _playerService = playerService;
        _lobbyService = lobbyService;
        SessionPlayer = null;
        PlayerTelegramId = 0;
    }
    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(IPlayerService))
        {
            return _playerService;
        }
        else if (serviceType == typeof(ILobbyService)) 
        {
            return _lobbyService;
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
