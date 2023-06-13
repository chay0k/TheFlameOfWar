namespace Contracts.Models;
public class Map
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Players { get; set; }
    public int SizeX { get; } = 15;
    public int SizeY { get; } = 20;
    public List<Cell> Cells { get; set; }
    public Map(int sizeX = 10, int sizeY = 15)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        Cells = new List<Cell>();
        CreateCells();
    }
    private void CreateCells()
    {
        for (int i = 0; i < SizeX; i++)
            for (int j = 0; j < SizeY; j++)
            {
                Cells.Add(new Cell(i, j, this));
            }
    }
    public Cell Cell(int x, int y)
    {
        return Cells.FirstOrDefault(с => с.X == x && с.Y == y);
    }
    public void Print(string field)
    {
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                if (field.ToLower() == "land")
                    Console.Write(Cell(i, j).Land.Emoji);
                else if (field.ToLower() == "resource")
                    Console.Write(Cell(i, j).Resource.Emoji);
            }
            Console.WriteLine();
        }
    }

    public void SetCell(int x, int y, Cell cell)
    {
        Cell targetCell = Cell(x, y);
        targetCell.Land = cell.Land;
        targetCell.Resource = cell.Resource;
        targetCell.Id = cell.Id;
        targetCell.Enemy = cell.Enemy;
    }
}
