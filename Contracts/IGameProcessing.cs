using Contracts.Models;

namespace Contracts;
public interface IGameProcessing
{
    public List<GameCommands> GetGameCommands(InnerParametres innerParametres);
    public List<Cell> GetAvailableCells(InnerParametres innerParametres);
    public Answer Move(InnerParametres innerParametres, Cell cell);
    public Answer ProcessCommand(InnerParametres innerParametres, GameCommands command);
}
