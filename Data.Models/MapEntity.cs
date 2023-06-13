namespace Data.Models;
public class MapEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Players { get; set; }
    public int SizeX { get; } = 0;
    public int SizeY { get; } = 0;
    public List<CellEntity> Cells { get; set; }
    public List<LobbyEntity> Lobbies { get; set; }
}
