using Contracts;
using Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servisces;

public enum GameState { NotStarted, InProgress, Finished }

public class LobbyService : ILobbyService
{
    private static readonly Dictionary<string, Lobby> _lobbies = new Dictionary<string, Lobby>();

    public void AddLobby(Lobby lobby)
    {
        // Додати лобі до словника _lobbies
        _lobbies[lobby.Id] = lobby;
    }

    public List<Lobby> GetAvailableLobbies()
    {
        // Повернути список доступних лобі
        return _lobbies.Values.ToList();
    }
}
