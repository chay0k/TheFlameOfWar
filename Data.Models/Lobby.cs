namespace Data.Models;
public class Lobby
{

    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";
    public Guid MapId { get; set; }
    public bool IsOpen { get; set; }
}

