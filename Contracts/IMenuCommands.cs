using Contracts.Models;

namespace Contracts;
public interface IMenuProcessing
{
    public List<MenuCommands> GetAvailableCommands(InnerParametres innerParametres);
    public Answer ProcessCommand(InnerParametres innerParametres, MenuCommands command);
    public List<Map> GetMapList(InnerParametres innerParametres);
}
