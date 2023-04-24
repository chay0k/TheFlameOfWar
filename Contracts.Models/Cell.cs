using Data.Models;

namespace Contracts.Models;
public class Cell
{ 
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Resource Resource { get; set; }
    public Enemy Enemy { get; set; }
    public Land Land { get; set; }
    public Map Map { get; set; }
    public List<Cell> Neighbors() 
    { 
        return new List<Cell>();
    } 
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

}
