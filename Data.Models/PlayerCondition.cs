namespace Data.Models;
public class PlayerCondition
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TableId { get; set; }
    public bool IsActive { get; set; }
    public int FullSpeed { get; set; }
    public int AvailableSpeed { get; set; }

}
