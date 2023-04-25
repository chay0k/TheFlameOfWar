using Data.Models;
using System.Threading.Tasks;
using Data.Models;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;
public class PlayerRepository: GenericRepository<PlayerEntity>, IPlayerRepository
{
    private readonly GameDbContext _context;

    public PlayerRepository(UnitOfWork unitOfWork) : base(unitOfWork.context)
    {
        _context = unitOfWork.context;
    }

    public async Task<PlayerEntity> GetByTelegramIdAsync(long telegramId)
    {
        return await GetSingleAsync(x => x.TelegramId == telegramId);
    }

    public async Task<PlayerEntity> GetByNameAsync(string name)
    {
        return await GetSingleAsync(x => x.Name == name);
    }

    public async Task<PlayerEntity> GetSingleAsync(Expression<Func<PlayerEntity, bool>> predicate)
    {
        return await context.Set<PlayerEntity>().SingleOrDefaultAsync(predicate);
    }
}
