using Data.Contexts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;
public class UnitOfWork : IDisposable
{
    private GameContext context = new GameContext();
    private GenericRepository<Map> mapRepository;
    private GenericRepository<Cell> cellRepository;
    private GenericRepository<GameLog> gameLogRepository;
    private GenericRepository<Land> landRepository;
    private GenericRepository<Lobby> lobbyRepository;
    private GenericRepository<LobbyCell> lobbyCellRepository;
    private GenericRepository<PlayerCondition> playerConditionRepository;
    private GenericRepository<Resource> resourceRepository;
    private GenericRepository<Thing> thingRepository;
    private GenericRepository<Unit> unitRepository;
    private GenericRepository<Player> playerRepository;
    private GenericRepository<PlayerSession> playerSessionRepository;

    public GenericRepository<Map> MapRepository
    {
        get
        {
            if (this.mapRepository == null)
            {
                this.mapRepository = new GenericRepository<Map>(context);
            }
            return mapRepository;
        }
    }
    public GenericRepository<Cell> CellRepository
    {
        get
        {
            if (this.cellRepository == null)
            {
                this.cellRepository = new GenericRepository<Cell>(context);
            }
            return cellRepository;
        }
    }
    public GenericRepository<GameLog> GameLogRepository
    {
        get
        {
            if (this.gameLogRepository == null)
            {
                this.gameLogRepository = new GenericRepository<GameLog>(context);
            }
            return gameLogRepository;
        }
    }
    public GenericRepository<Land> LandRepository
    {
        get
        {
            if (this.landRepository == null)
            {
                this.landRepository = new GenericRepository<Land>(context);
            }
            return landRepository;
        }
    }
    public GenericRepository<Lobby> LobbyRepository
    {
        get
        {
            if (this.lobbyRepository == null)
            {
                this.lobbyRepository = new GenericRepository<Lobby>(context);
            }
            return lobbyRepository;
        }
    }
    public GenericRepository<LobbyCell> LobbyCellRepository
    {
        get
        {
            if (this.lobbyCellRepository == null)
            {
                this.lobbyCellRepository = new GenericRepository<LobbyCell>(context);
            }
            return lobbyCellRepository;
        }
    }
    public GenericRepository<PlayerCondition> PlayerConditionRepository
    {
        get
        {
            if (this.playerConditionRepository == null)
            {
                this.playerConditionRepository = new GenericRepository<PlayerCondition>(context);
            }
            return playerConditionRepository;
        }
    }
    public GenericRepository<Resource> ResourceRepository
    {
        get
        {
            if (this.resourceRepository == null)
            {
                this.resourceRepository = new GenericRepository<Resource>(context);
            }
            return resourceRepository;
        }
    }
    public GenericRepository<Thing> ThingRepository
    {
        get
        {
            if (this.thingRepository == null)
            {
                this.thingRepository = new GenericRepository<Thing>(context);
            }
            return thingRepository;
        }
    }
    public GenericRepository<Unit> UnitRepository
    {
        get
        {
            if (this.unitRepository == null)
            {
                this.unitRepository = new GenericRepository<Unit>(context);
            }
            return unitRepository;
        }
    }
    public GenericRepository<Player> PlayerRepository
    {
        get
        {
            if (this.playerRepository == null)
            {
                this.playerRepository = new GenericRepository<Player>(context);
            }
            return playerRepository;
        }
    }
    public GenericRepository<PlayerSession> PlayerSessionRepository
    {
        get
        {
            if (this.playerSessionRepository == null)
            {
                this.playerSessionRepository = new GenericRepository<PlayerSession>(context);
            }
            return playerSessionRepository;
        }
    }

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
