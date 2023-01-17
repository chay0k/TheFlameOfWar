using Data.Contexts;
using Data.Models;

namespace Data.Repositories;
public interface IUnitOfWork
{
    public GenericRepository<Map> MapRepository { get; }
    public GenericRepository<Cell> CellRepository { get; }
    public GenericRepository<GameLog> GameLogRepository { get; }
    public GenericRepository<Land> LandRepository { get; }
    public GenericRepository<Lobby> LobbyRepository { get; }
    public GenericRepository<LobbyCell> LobbyCellRepository { get;}
    public GenericRepository<PlayerCondition> PlayerConditionRepository { get;}
    public GenericRepository<Resource> ResourceRepository { get; }
    public GenericRepository<Thing> ThingRepository { get; }
    public GenericRepository<Unit> UnitRepository { get; }
    public GenericRepository<Player> PlayerRepository { get; }
    public GenericRepository<PlayerSession> PlayerSessionRepository { get; }

}
