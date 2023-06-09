//using Contracts.Models;
using Data.Contexts;
using Data.Models;


namespace Data.Repositories;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    public GameDbContext Context = new GameDbContext();

    private GenericRepository<BuildingEntity> _buildingRepository;
    private GenericRepository<CellEntity> _cellRepository;
    private GenericRepository<CityEntity> _cityRepository;
    private GenericRepository<CityBuildingEntity> _cityBuildingRepository;
    private GenericRepository<GameLogEntity> _gameLogRepository;
    private GenericRepository<GodEntity> _godRepository;
    private GenericRepository<GuardEntity> _guardRepository;
    private GenericRepository<GuardUnitListEntity> _guardUnitListRepository;
    private GenericRepository<LandEntity> _landRepository;
    private GenericRepository<LobbyEntity> _lobbyRepository;
    private GenericRepository<LobbyCellEntity> _lobbyCellRepository;
    private GenericRepository<MapEntity> _mapRepository;
    private GenericRepository<PanteonEntity> _panteonRepository;
    private GenericRepository<PlayerConditionEntity> _playerConditionRepository;
    private GenericRepository<PlayerEntity> _playerRepository;
    private GenericRepository<ResourceEntity> _resourceRepository;
    private GenericRepository<UnitEntity> _unitRepository;

    public GenericRepository<MapEntity> MapRepository => _mapRepository ??= new GenericRepository<MapEntity>(Context);
    public GenericRepository<CellEntity> CellRepository =>_cellRepository ??= new GenericRepository<CellEntity>(Context);
    public GenericRepository<GameLogEntity> GameLogRepository => _gameLogRepository ??= new GenericRepository<GameLogEntity>(Context);
    public GenericRepository<LandEntity> LandRepository => _landRepository ??= new GenericRepository<LandEntity>(Context);
    public GenericRepository<LobbyEntity> LobbyRepository => _lobbyRepository ??= new GenericRepository<LobbyEntity>(Context);
    public GenericRepository<LobbyCellEntity> LobbyCellRepository => _lobbyCellRepository ??= new GenericRepository<LobbyCellEntity>(Context);
    public GenericRepository<PlayerConditionEntity> PlayerConditionRepository => _playerConditionRepository ??= new GenericRepository<PlayerConditionEntity>(Context);
    public GenericRepository<ResourceEntity> ResourceRepository => _resourceRepository ??= new GenericRepository<ResourceEntity>(Context);
    public GenericRepository<BuildingEntity> BuildingRepository => _buildingRepository ??= new GenericRepository<BuildingEntity>(Context);
    public GenericRepository<UnitEntity> UnitRepository => _unitRepository ??= new GenericRepository<UnitEntity>(Context);
    public GenericRepository<PlayerEntity> PlayerRepository => _playerRepository ??= new GenericRepository<PlayerEntity>(Context);
    public GenericRepository<CityEntity> CityRepository => _cityRepository = new GenericRepository<CityEntity>(Context);
    public GenericRepository<CityBuildingEntity> CityBuildingRepository => _cityBuildingRepository ??= new GenericRepository<CityBuildingEntity>(Context);
    public GenericRepository<GodEntity> GodRepository => _godRepository ??= new GenericRepository<GodEntity>(Context);
    public GenericRepository<GuardEntity> GuardRepository => _guardRepository ??= new GenericRepository<GuardEntity>(Context);
    public GenericRepository<GuardUnitListEntity> GuardUnitListRepository => _guardUnitListRepository ??= new GenericRepository<GuardUnitListEntity>(Context);
    public GenericRepository<PanteonEntity> PanteonRepository => _panteonRepository ??= new GenericRepository<PanteonEntity>(Context);

    public void Save()
    {
        Context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose ()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
