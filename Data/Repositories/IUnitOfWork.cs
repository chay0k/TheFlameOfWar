using Data.Contexts;
using Data.Models;

namespace Data.Repositories;
public interface IUnitOfWork
{
    public GenericRepository<BuildingEntity> BuildingRepository { get; }
    public GenericRepository<CellEntity> CellRepository { get; }
    public GenericRepository<CityBuildingEntity> CityBuildingRepository { get; }
    public GenericRepository<CityEntity> CityRepository { get; }
    public GenericRepository<GameLogEntity> GameLogRepository { get; }
    public GenericRepository<GodEntity> GodRepository { get; }
    public GenericRepository<GuardEntity> GuardRepository { get; }
    public GenericRepository<GuardUnitListEntity> GuardUnitListRepository { get; }
    public GenericRepository<LandEntity> LandRepository { get; }
    public GenericRepository<LobbyCellEntity> LobbyCellRepository { get;}
    public GenericRepository<LobbyEntity> LobbyRepository { get; }
    public GenericRepository<MapEntity> MapRepository { get; }
    public GenericRepository<PanteonEntity> PanteonRepository { get; }
    public GenericRepository<PlayerConditionEntity> PlayerConditionRepository { get;}
    public GenericRepository<PlayerEntity> PlayerRepository { get; }
    public GenericRepository<ResourceEntity> ResourceRepository { get; }
    public GenericRepository<UnitEntity> UnitRepository { get; }

}
