//using Contracts.Models;
using Data.Contexts;
using Data.Models;


namespace Data.Repositories;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    public GameDbContext context = new GameDbContext();

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

    public GenericRepository<MapEntity> MapRepository => _mapRepository ??= new GenericRepository<MapEntity>(context);
    public GenericRepository<CellEntity> CellRepository =>_cellRepository ??= new GenericRepository<CellEntity>(context);
    public GenericRepository<GameLogEntity> GameLogRepository => _gameLogRepository ??= new GenericRepository<GameLogEntity>(context);
    public GenericRepository<LandEntity> LandRepository => _landRepository ??= new GenericRepository<LandEntity>(context);
    public GenericRepository<LobbyEntity> LobbyRepository => _lobbyRepository ??= new GenericRepository<LobbyEntity>(context);
    public GenericRepository<LobbyCellEntity> LobbyCellRepository => _lobbyCellRepository ??= new GenericRepository<LobbyCellEntity>(context);
    public GenericRepository<PlayerConditionEntity> PlayerConditionRepository => _playerConditionRepository ??= new GenericRepository<PlayerConditionEntity>(context);
    public GenericRepository<ResourceEntity> ResourceRepository => _resourceRepository ??= new GenericRepository<ResourceEntity>(context);
    public GenericRepository<BuildingEntity> BuildingRepository => _buildingRepository ??= new GenericRepository<BuildingEntity>(context);
    public GenericRepository<UnitEntity> UnitRepository => _unitRepository ??= new GenericRepository<UnitEntity>(context);
    public GenericRepository<PlayerEntity> PlayerRepository => _playerRepository ??= new GenericRepository<PlayerEntity>(context);
    public GenericRepository<CityEntity> CityRepository => _cityRepository = new GenericRepository<CityEntity>(context);
    public GenericRepository<CityBuildingEntity> CityBuildingRepository => _cityBuildingRepository ??= new GenericRepository<CityBuildingEntity>(context);
    public GenericRepository<GodEntity> GodRepository => _godRepository ??= new GenericRepository<GodEntity>(context);
    public GenericRepository<GuardEntity> GuardRepository => _guardRepository ??= new GenericRepository<GuardEntity>(context);
    public GenericRepository<GuardUnitListEntity> GuardUnitListRepository => _guardUnitListRepository ??= new GenericRepository<GuardUnitListEntity>(context);
    public GenericRepository<PanteonEntity> PanteonRepository => _panteonRepository ??= new GenericRepository<PanteonEntity>(context);

    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
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
