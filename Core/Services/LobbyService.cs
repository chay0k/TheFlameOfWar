using Contracts;
using Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public enum GameState { NotStarted, InProgress, Finished }

public class LobbyService : ILobbyService
{
    private static readonly Dictionary<string, Lobby> _lobbies = new Dictionary<string, Lobby>();

    public void AddLobby(Lobby lobby)
    {
        _lobbies[lobby.Id] = lobby;
    }

    public List<Lobby> GetAvailableLobbies()
    {
        // Повернути список доступних лобі
        return _lobbies.Values.ToList();
    }
    public Lobby GetByPlayer(Player player) 
    {
        return _lobbies.Values.FirstOrDefault(l => l.Players.Contains(player));
    }

    public Lobby GetByToken(string token)
    {
        return _lobbies.Values.FirstOrDefault(l => l.Token == token);
    }

    public void DeleteLobby(Lobby lobby)
    {
        _lobbies.Remove(_lobbies.FirstOrDefault(x => x.Value == lobby).Key);
    }

    public string DeletePlayerFromLobby(Player player)
    {
        var message = "";
        var existingLobby = GetByPlayer(player);
        if (existingLobby != null)
        {
            var lobbyName = existingLobby.Name;
            if (existingLobby.Players.Contains(player))
            {
                existingLobby.Players.Remove(player);
                if (existingLobby.Players.Count == 0)
                {
                    DeleteLobby(existingLobby);
                    message = $"Lobby {lobbyName} deleted.";
                }
                else
                {
                    message = $"player {player.Name} deleted from {lobbyName} lobby.";
                }
            }
        }

        return message;
    }
}
