using Data.Contexts;
using Data.Models;


namespace Data.Repositories;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    private GameDbContext context = new GameDbContext();
    private GenericRepository<MapEntity> _mapRepository;
    private GenericRepository<CellEntity> _cellRepository;
    private GenericRepository<GameLogEntity> _gameLogRepository;
    private GenericRepository<LandEntity> _landRepository;
    private GenericRepository<LobbyEntity> _lobbyRepository;
    private GenericRepository<LobbyCellEntity> _lobbyCellRepository;
    private GenericRepository<PlayerConditionEntity> _playerConditionRepository;
    private GenericRepository<ResourceEntity> _resourceRepository;
    private GenericRepository<ThingEntity> _thingRepository;
    private GenericRepository<UnitEntity> _unitRepository;
    private GenericRepository<PlayerEntity> _playerRepository;
    private GenericRepository<PlayerSessionEntity> _playerSessionRepository;
    private GenericRepository<CommandEntity> _commandRepository;
    private GenericRepository<LastCommandEntity> _lastCommandRepository;

    public GenericRepository<MapEntity> MapRepository => _mapRepository ??= new GenericRepository<MapEntity>(context);
    public GenericRepository<CellEntity> CellRepository =>_cellRepository ??= new GenericRepository<CellEntity>(context);
    public GenericRepository<GameLogEntity> GameLogRepository => _gameLogRepository ??= new GenericRepository<GameLogEntity>(context);
    public GenericRepository<LandEntity> LandRepository => _landRepository ??= new GenericRepository<LandEntity>(context);
    public GenericRepository<LobbyEntity> LobbyRepository => _lobbyRepository ??= new GenericRepository<LobbyEntity>(context);
    public GenericRepository<LobbyCellEntity> LobbyCellRepository => _lobbyCellRepository ??= new GenericRepository<LobbyCellEntity>(context);
    public GenericRepository<PlayerConditionEntity> PlayerConditionRepository => _playerConditionRepository ??= new GenericRepository<PlayerConditionEntity>(context);
    public GenericRepository<ResourceEntity> ResourceRepository => _resourceRepository ??= new GenericRepository<ResourceEntity>(context);
    public GenericRepository<ThingEntity> ThingRepository => _thingRepository ??= new GenericRepository<ThingEntity>(context);
    public GenericRepository<UnitEntity> UnitRepository => _unitRepository ??= new GenericRepository<UnitEntity>(context);
    public GenericRepository<PlayerEntity> PlayerRepository => _playerRepository ??= new GenericRepository<PlayerEntity>(context);
    public GenericRepository<PlayerSessionEntity> PlayerSessionRepository => _playerSessionRepository = new GenericRepository<PlayerSessionEntity>(context);
    public GenericRepository<CommandEntity> CommandRepository => _commandRepository ??= new GenericRepository<CommandEntity>(context);
    public GenericRepository<LastCommandEntity> LastCommandRepository => _lastCommandRepository ??= new GenericRepository<LastCommandEntity>(context);

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
