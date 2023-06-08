using Contracts;
using Contracts.Models;
using Core.Commands;
using Core.Commands.LobbyCommands;

namespace Core.Services;
public class CommandService : ICommandService
{
    public Guid Id { get; set; }
    private readonly Dictionary<string, ICommand> _commands;
    private readonly Stack<(ICommand, string)> _commandsStack;
    //private readonly ILobbyService _lobbyService;
    //private readonly IPlayerService _playerService;
    //private readonly IMapService _mapService;

    public CommandService(ILobbyService lobbyService, IPlayerService playerService, IMapService mapService)
    {
        //_lobbyService = lobbyService;
        //_playerService = playerService;
        //_mapService = mapService;
        _commands = new Dictionary<string, ICommand>
            {
                { Constants.Start,          new StartCommand() },
                { Constants.HotSeat,        new HotSeat() },
                { Constants.Continue,       new BackToCurrentGame() },
                { Constants.NewGame,        new NewGame(lobbyService, this) },
                { Constants.Connect,        new ConnectToExist(lobbyService, this) },
                { Constants.MapChoose,      new ChooseMap() },
                { Constants.MapCreate,      new CreateMap() },
                { Constants.Info,           new Info() },
                { Constants.Back,           new StepBack() },
                { Constants.Menu,           new MainMenu() },
                { Constants.Name,           new Name(playerService, this) },

            };
        _commandsStack = new Stack<(ICommand, string)>();
        Id = Guid.NewGuid();
        _commandsStack = new Stack<(ICommand, string)>();
    }    
    public bool ExpectedInput { get; set; } = false;
    public ICommand FindCommand(ref string text)
    {
        var command = FindCommandEnum(text);
        if (command != null && _commands.TryGetValue(command, out var commandObject))
        {
            text = text.Replace(command, "").TrimStart(' ');
            return commandObject; 
        }
        else
            return null;
    }
    public string FindCommandEnum(string text)
    {
        var firstWord = text.Split(' ')[0].ToLower();
        return firstWord;
    }
    public (ICommand, string) PeekCommand()
    {
        if (_commandsStack.Count() == 0)
            return (null, "");
        return _commandsStack.Peek();
    }
    public void PushCommand(ICommand command, string argument)
    {
        if (_commandsStack.Count > 0 && _commandsStack.Peek().Item1 == command)
            return;
        _commandsStack.Push((command, argument));
    }
    public (ICommand, string) PopCommand()
    {
        if (_commandsStack.Count() == 0)
            return (null, "");
        return _commandsStack.Pop();
    }
    public void ClearCommands()
    {
        _commandsStack.Clear();
    }
}
