using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;
public interface IPlayerRepository : IRepository<PlayerEntity>
{
    Task<PlayerEntity> GetByTelegramIdAsync(long telegramId);
    Task<PlayerEntity> GetByNameAsync(string name);
}
