namespace Contracts.Models;
public class Map
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Players { get; set; }
    public int SizeX { get; } = 0;
    public int SizeY { get; } = 0;
    public List<Cell> Cells { get; set; }

}
