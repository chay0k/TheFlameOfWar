using Contracts.Models;

namespace Contracts;
public interface ILobbyService
{
    private static readonly Dictionary<string, Lobby> _lobbies = new Dictionary<string, Lobby>();
    public void AddLobby(Lobby lobby);
    public void DeleteLobby(Lobby lobby);
    public List<Lobby> GetAvailableLobbies();
    public Lobby GetByPlayer(Player player);
    public Lobby GetByToken(string token); 
    public string DeletePlayerFromLobby(Player player);
}
