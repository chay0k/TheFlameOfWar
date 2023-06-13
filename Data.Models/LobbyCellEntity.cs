namespace Data.Models;
public class LobbyCellEntity
{
    public Guid Id { get; set; }
    public CellEntity Cell { get; set; }
    public LobbyEntity Lobby { get; set; }
    public bool IsOpened { get; set; }
    public ResourceEntity? Resource { get; set; }
    public GuardEntity? Guard { get; set; }

}