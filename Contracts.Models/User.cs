namespace Contracts.Models;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long TelegramId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

