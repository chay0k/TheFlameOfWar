using Contracts;
using Contracts.Models;
using Data.Models;
using Data.Repositories;

namespace Core;
public class UserService: IUserService
{
    private IUnitOfWork _unitOfWork = new UnitOfWork();
    public User GetUser(long telegramId)
    {
        var playerRepository = _unitOfWork.PlayerRepository;
        var player = playerRepository.GetAsync().Result.Where(x => x.TelegramId == telegramId).FirstOrDefault();
        if (player == null)
            return null;
        return TranformToUser(player);
    }
    public User GetUser(string telegramId)
    {
        var longId = long.Parse(telegramId);
        return GetUser(longId);
    }
    public User GetUser(Guid id)
    {
        var playerRepository = _unitOfWork.PlayerRepository;
        var player = playerRepository.GetAsync().Result.Where(x => x.Id == id).FirstOrDefault();
        return TranformToUser(player);
    }

    public async Task CreateNewAsync(string name, long telegramId = 0)
    {
        var newUser = new User() { Name = name, TelegramId = telegramId };
        var player = TransformToPlayer(newUser);
        await _unitOfWork.PlayerRepository.InsertAsync(player);
    }

    private User TranformToUser(Player player)
    {
        if (player == null) return new User();
        return new User() 
        { 
            Name = player.Name, 
            TelegramId = player.TelegramId, 
            FirstName = player.FirstName,
            LastName = player.LastName,
            Id = player.Id
        };
    }

    private Player TransformToPlayer (User user)
    {
        if(user == null) return new Player();
        return new Player()
        {
            Id = user.Id,
            Name = user.Name,
            FirstName = user.FirstName,
            LastName = user.LastName,
            TelegramId = user.TelegramId,
        };
    }
}
