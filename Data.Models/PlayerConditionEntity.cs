namespace Data.Models;
public class PlayerConditionEntity
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerEntity Player { get; set; }
    public Guid LobbyEntityId { get; set; }
    public virtual LobbyEntity Lobby { get; set; }
    public int PanteonEntityId { get; set; }
    public virtual PanteonEntity Panteon { get; set; }
    public int CityId { get; set; }
    public virtual CityEntity City { get; set; }
    public int GodId { get; set; }
    public virtual GodEntity God { get; set; }

}
