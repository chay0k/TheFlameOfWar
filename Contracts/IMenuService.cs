using Contracts.Models;

namespace Contracts;
public interface IMenuService
{
    public Task<List<MenuCommands>> GetAvailableCommandsAsync(InnerParametres innerParametres);
    public Answer ProcessCommand(InnerParametres innerParametres, MenuCommands command);
    public List<Map> GetMapList(InnerParametres innerParametres);
}
