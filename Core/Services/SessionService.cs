using Contracts;
using Contracts.Models;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Core.Services;
public class SessionService : ISessionService
{
    private readonly Dictionary<string, object> _sessionData = new Dictionary<string, object>();
    private readonly IServiceProvider _serviceProvider;

    public SessionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

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
    public object GetData(string key)
    {
        return _sessionData.GetValueOrDefault(key);
    }
    public void SetData(string key, object value)
    {
        _sessionData[key] = value;
    }
}
