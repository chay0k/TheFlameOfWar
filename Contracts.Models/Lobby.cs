namespace Contracts.Models;
public class Lobby
{

    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";
    public Map Map { get; set; }
    public bool Open { get; set; }
}

