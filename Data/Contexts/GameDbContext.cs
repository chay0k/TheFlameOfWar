using Microsoft.EntityFrameworkCore;
using Data.Models;
using System.Configuration;

namespace Data.Contexts;

public class GameDbContext : DbContext
{
    public DbSet<BuildingEntity> Buildings { get; set; }
    public DbSet<CellEntity> Cells { get; set; }
    public DbSet<CityBuildingEntity> CityBuildings { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<GameLogEntity> GameLog { get; set; }
    public DbSet<GodEntity> Gods { get; set; }
    public DbSet<GuardEntity> Guards { get; set; }
    public DbSet<GuardUnitListEntity> GuardUnitLists { get; set; }
    public DbSet<LandEntity> Lands { get; set; }
    public DbSet<LobbyCellEntity> TableCells { get; set; }
    public DbSet<LobbyEntity> Tables { get; set; }
    public DbSet<MapEntity> Maps { get; set; }
    public DbSet<PanteonEntity> Panteons { get; set; }
    public DbSet<PlayerConditionEntity> PlayerConditions { get; set; }
    public DbSet<PlayerEntity> Players { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public GameDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerConditionEntity>()
            .HasOne(pc => pc.God)
            .WithMany(g => g.PlayerConditions)
            .HasForeignKey(pc => pc.GodId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerConditionEntity>()
        .HasOne(pc => pc.Lobby)
        .WithMany(g => g.PlayerConditions)
        .HasForeignKey(pc => pc.LobbyEntityId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerConditionEntity>()
        .HasOne(pc => pc.Panteon)
        .WithMany(g => g.PlayerConditions)
        .HasForeignKey(pc => pc.PanteonEntityId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerConditionEntity>()
        .HasOne(pc => pc.City)
        .WithMany(g => g.PlayerConditions)
        .HasForeignKey(pc => pc.CityId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerConditionEntity>()
        .HasOne(pc => pc.Player)
        .WithMany(g => g.PlayerConditions)
        .HasForeignKey(pc => pc.PlayerId)
        .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<GuardUnitListEntity>()
            .HasKey(g => g.Id); // Встановлення первинного ключа

        modelBuilder.Entity<GuardUnitListEntity>()
            .HasOne(gu => gu.Guard) // Відносини з GuardEntity
            .WithMany(g => g.GuardUnitLists) // Один-до-багатьох, зв'язок з GuardEntity.GuardUnitLists
            .HasForeignKey(gu => gu.GuardId); // Зовнішній ключ для GuardEntity

        modelBuilder.Entity<GuardUnitListEntity>()
            .HasOne(gu => gu.Unit) // Відносини з UnitEntity
            .WithMany(u => u.GuardUnitLists) // Багато-до-багатьох, зв'язок з UnitEntity.GuardUnitLists
            .HasForeignKey(gu => gu.UnitId); // Зовнішній ключ для UnitEntity

        modelBuilder.Entity<CellEntity>()
            .HasOne(c => c.Map)
            .WithMany(m => m.Cells)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LobbyEntity>()
            .HasOne(l => l.Map)
            .WithMany(m => m.Lobbies)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LobbyCellEntity>()
            .HasOne(lc => lc.Cell)
            .WithMany(c => c.LobbyCells)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LobbyCellEntity>()
            .HasOne(lc => lc.Lobby)
            .WithMany(l => l.LobbyCells)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
}
