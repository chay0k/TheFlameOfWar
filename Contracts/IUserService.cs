using Contracts.Models;

namespace Contracts;
public interface IUserService
{
    User GetUser(long telegramId);
    User GetUser(string telegramId);
    User GetUser(Guid id);
}
