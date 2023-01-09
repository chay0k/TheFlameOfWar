using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Contexts;

public class GameContext : DbContext
{
    public DbSet<PlayerCondition> PlayerConditions { get; set; }
    public DbSet<Land> Lands { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Thing> Things { get; set; }
    public DbSet<Map> Maps { get; set; }
    public DbSet<Cell> Cells { get; set; }
    public DbSet<LobbyCell> TableCells { get; set; }
    public DbSet<Lobby> Tables { get; set; }
    public DbSet<Player> Users { get; set; }
    public DbSet<PlayerSession> UserSessions { get; set; }
    public DbSet<GameLog> GameLog { get; set; }

    //public GameContext() => Database.EnsureDeleted();
    public GameContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=theflamesofwar.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
