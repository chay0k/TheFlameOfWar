namespace Contracts.Models;
public class Player : IEquatable<Player>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long TelegramId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public bool Equals(Player other)
    {
        if (other == null)
            return false;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj.GetType() != typeof(Player))
            return false;
        return Equals(obj as Player);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

