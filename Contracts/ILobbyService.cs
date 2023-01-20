using Contracts.Models;

namespace Contracts;
public interface ILobbyService
{
    public List<LobbyCommands> GetAvailableLobbyCommands(InnerParametres innerParametres);
    public Answer ProcessCommand(InnerParametres innerParametres, LobbyCommands command);
    public void SetPlayerNumber(InnerParametres innerParametres, int newNumber = 0);

}
