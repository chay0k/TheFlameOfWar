using Data.Contexts;
using Data.Models;


namespace Data.Repositories;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    private GameDbContext context = new GameDbContext();
    private GenericRepository<Map> _mapRepository;
    private GenericRepository<Cell> _cellRepository;
    private GenericRepository<GameLog> _gameLogRepository;
    private GenericRepository<Land> _landRepository;
    private GenericRepository<Lobby> _lobbyRepository;
    private GenericRepository<LobbyCell> _lobbyCellRepository;
    private GenericRepository<PlayerCondition> _playerConditionRepository;
    private GenericRepository<Resource> _resourceRepository;
    private GenericRepository<Thing> _thingRepository;
    private GenericRepository<Unit> _unitRepository;
    private GenericRepository<Player> _playerRepository;
    private GenericRepository<PlayerSession> _playerSessionRepository;
    private GenericRepository<Command> _commandRepository;
    private GenericRepository<LastCommand> _lastCommandRepository;

    public GenericRepository<Map> MapRepository => _mapRepository ??= new GenericRepository<Map>(context);
    public GenericRepository<Cell> CellRepository =>_cellRepository ??= new GenericRepository<Cell>(context);
    public GenericRepository<GameLog> GameLogRepository => _gameLogRepository ??= new GenericRepository<GameLog>(context);
    public GenericRepository<Land> LandRepository => _landRepository ??= new GenericRepository<Land>(context);
    public GenericRepository<Lobby> LobbyRepository => _lobbyRepository ??= new GenericRepository<Lobby>(context);
    public GenericRepository<LobbyCell> LobbyCellRepository => _lobbyCellRepository ??= new GenericRepository<LobbyCell>(context);
    public GenericRepository<PlayerCondition> PlayerConditionRepository => _playerConditionRepository ??= new GenericRepository<PlayerCondition>(context);
    public GenericRepository<Resource> ResourceRepository => _resourceRepository ??= new GenericRepository<Resource>(context);
    public GenericRepository<Thing> ThingRepository => _thingRepository ??= new GenericRepository<Thing>(context);
    public GenericRepository<Unit> UnitRepository => _unitRepository ??= new GenericRepository<Unit>(context);
    public GenericRepository<Player> PlayerRepository => _playerRepository ??= new GenericRepository<Player>(context);
    public GenericRepository<PlayerSession> PlayerSessionRepository => _playerSessionRepository = new GenericRepository<PlayerSession>(context);
    public GenericRepository<Command> CommandRepository => _commandRepository ??= new GenericRepository<Command>(context);
    public GenericRepository<LastCommand> LastCommandRepository => _lastCommandRepository ??= new GenericRepository<LastCommand>(context);

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
