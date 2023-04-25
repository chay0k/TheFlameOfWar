using Contracts.Models;

namespace Contracts;
public interface IPlayerService
{
    Task<Player> GetPlayerAsync(long telegramId);
    Task<Player> GetPlayerAsync(string telegramId);
    Task<Player> GetPlayerAsync(Guid id);
    Task<Player> GetPlayerByNameAsync(string Name);
    Task<Player> CreateNewAsync(string name, long telegramId = 0);
}
