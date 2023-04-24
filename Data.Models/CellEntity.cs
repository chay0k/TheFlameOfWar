namespace Data.Models;
public class CellEntity
{
    public Guid Id { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public ResourceEntity? Resource { get; set; }
    public GuardEntity? Guard { get; set; }
    public LandEntity Land { get; set; }
    public MapEntity Map { get; set; }
    public int PlayersStartPosition { get; set; }
    public List<LobbyCellEntity> LobbyCells { get; set; }
}
