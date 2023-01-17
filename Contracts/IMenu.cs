using Contracts.Models;

namespace Contracts;
public interface IMenu
{
    public List<MenuCommands> GetAvailableCommands(User user);
    public Answer ProcessCommand(InnerParametres innerParametres, MenuCommands command);
    public List<Map> GetMapList(InnerParametres innerParametres);
}
