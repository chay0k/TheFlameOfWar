using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PlayerRepository : GenericRepository<PlayerEntity>, IPlayerRepository
    {
        private readonly GameDbContext _context;

        public PlayerRepository(UnitOfWork unitOfWork) : base(unitOfWork.Context)
        {
            _context = unitOfWork.Context;
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
            return await _context.Set<PlayerEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
