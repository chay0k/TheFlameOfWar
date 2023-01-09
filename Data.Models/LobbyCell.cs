namespace Data.Models;
public class LobbyCell
{
    public Guid Id { get; set; }
    public Guid CellId { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public Guid ThingId { get; set; }
    public Guid LandId { get; set; }
    public Guid MapID { get; set; }
    public Guid TableId { get; set; }
    public bool IsOpen { get; set; }
}