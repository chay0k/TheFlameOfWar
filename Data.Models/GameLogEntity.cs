using System.Numerics;

namespace Data.Models;
public class GameLogEntity
{
    public Guid Id { get; set; }
    public PlayerEntity Player { get; set; }
    public LobbyEntity Lobby { get; set; }
    public int PlayerNumber { get; set; }
    public int StepNumber { get; set; }
    public string Operation { get; set; } = "";
    public string Description { get; set; } = "";
    DateTime DateTime { get; set; }
}
