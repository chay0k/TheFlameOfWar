using Contracts.Models;


namespace Data.Models;
public class Command
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CommandTypes CommandType { get; set; }
    public MenuCommands MenuCommand { get; set; }
    public GameCommands GameCommand { get; set; }
    public LobbyCommands LobbyCommand { get; set; }
}
