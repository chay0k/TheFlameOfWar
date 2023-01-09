namespace Data.Models;
public class Cell
{
    public Guid Id { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public Guid ThingId { get; set; }
    public Guid LandId { get; set; }
    public Guid MapID { get; set; }
    public int PlayerPosition { get; set; }
}
