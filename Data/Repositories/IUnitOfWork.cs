using Data.Contexts;
using Data.Models;

namespace Data.Repositories;
public interface IUnitOfWork
{
    public GenericRepository<MapEntity> MapRepository { get; }
    public GenericRepository<CellEntity> CellRepository { get; }
    public GenericRepository<GameLogEntity> GameLogRepository { get; }
    public GenericRepository<LandEntity> LandRepository { get; }
    public GenericRepository<LobbyEntity> LobbyRepository { get; }
    public GenericRepository<LobbyCellEntity> LobbyCellRepository { get;}
    public GenericRepository<PlayerConditionEntity> PlayerConditionRepository { get;}
    public GenericRepository<ResourceEntity> ResourceRepository { get; }
    public GenericRepository<ThingEntity> ThingRepository { get; }
    public GenericRepository<UnitEntity> UnitRepository { get; }
    public GenericRepository<PlayerEntity> PlayerRepository { get; }
    public GenericRepository<PlayerSessionEntity> PlayerSessionRepository { get; }
    public GenericRepository<LastCommandEntity> LastCommandRepository { get; }
    public GenericRepository<CommandEntity> CommandRepository { get; }

}
