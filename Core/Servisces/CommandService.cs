using Contracts;
using Contracts.Models;
using Core.Commands;

namespace Core.Servisces;
public class CommandService : ICommandService
{
    public Guid Id { get; set; }
    private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>
    {
    { Constants.Start,          new StartCommand() },
    { Constants.Continue,       new BackToCurrentGame() },
    { Constants.NewGame,        new NewGame() },
    { Constants.Connect,        new ConnectToExist() },
    { Constants.MapChoose,      new ChooseMap() },
    { Constants.MapCreate,      new CreateMap() },
    { Constants.Info,           new Info() },
    { Constants.Back,           new StepBack() },
    { Constants.Menu,           new MainMenu() },
    { Constants.Name,           new Name() },

};
    private Stack<(ICommand, string)> commands = new Stack<(ICommand, string)>();
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
        if (commands.Count() == 0)
            return (null, "");
        return commands.Peek();
    }
    public void PushCommand(ICommand command, string argument)
    {
        if (commands.Count > 0 && commands.Peek().Item1 == command)
            return;
        commands.Push((command, argument));
    }
    public (ICommand, string) PopCommand()
    {
        if (commands.Count() == 0)
            return (null, "");
        return commands.Pop();
    }
    public void ClearCommands()
    {
        commands.Clear();
    }
    public CommandService()
    {
        Id = Guid.NewGuid();
    }
}
