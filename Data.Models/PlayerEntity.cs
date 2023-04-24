namespace Data.Models;
public class PlayerEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long TelegramId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<PlayerConditionEntity> PlayerConditions { get; set; }
}

