using Contracts.Models;

namespace Data.Models;
public class PlayerSessionEntity
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public int Location { get; set; }
    public UserSteps UserStep { get; set; }
    public int PlayerNumber { get; set; }
    public Guid LobbyId { get; set; }
    public DateTime DateTime { get; set; }
}
