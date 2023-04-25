namespace Data.Models;
public class LobbyEntity
{

    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";
    public MapEntity Map { get; set; }
    public bool IsActive { get; set; }
    public List<PlayerEntity> Players { get; set; }
    public int ActivePlayerNumber { get; set; }
    public List<LobbyCellEntity> LobbyCells { get; set; }
    public virtual ICollection<PlayerConditionEntity> PlayerConditions { get; set; }
}

