using Contracts.Models;

namespace Contracts;
public interface ILobbyCommands
{
    public List<LobbyCommands> GetAvailableLobbyCommands(InnerParametres innerParametres);
    public Answer ProcessCommand(InnerParametres innerParametres, LobbyCommands command);
    public void SetPlayerNumber(InnerParametres innerParametres, int newNumber = 0);

}
