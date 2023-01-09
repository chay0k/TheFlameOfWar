namespace Contracts.Models;
public class Cell
{
    public Guid Id { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public Thing Thing { get; set; }
    public Land Land { get; set; }
    public Map Map { get; set; }
    public int PlayerPosition { get; set; }
    public Cell[] NeighboringCells { get; set; } = new Cell[6];
}
