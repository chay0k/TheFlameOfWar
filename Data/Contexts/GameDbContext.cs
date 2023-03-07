using Microsoft.EntityFrameworkCore;
using Data.Models;
using System.Configuration;

namespace Data.Contexts;

public class GameDbContext : DbContext
{
    public DbSet<PlayerConditionEntity> PlayerConditions { get; set; }
    public DbSet<LandEntity> Lands { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<ThingEntity> Things { get; set; }
    public DbSet<MapEntity> Maps { get; set; }
    public DbSet<CellEntity> Cells { get; set; }
    public DbSet<LobbyCellEntity> TableCells { get; set; }
    public DbSet<LobbyEntity> Tables { get; set; }
    public DbSet<PlayerEntity> Users { get; set; }
    public DbSet<PlayerSessionEntity> UserSessions { get; set; }
    public DbSet<GameLogEntity> GameLog { get; set; }

    //public GameDbContext() => Database.EnsureDeleted();
    public GameDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
                                
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //optionsBuilder.UseSqlite("Data Source=theflamesofwar.db");
        optionsBuilder.UseSqlite(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<PlayerEntity>(entity =>
        //{
        //    entity.HasOne
        //});
    }
}
