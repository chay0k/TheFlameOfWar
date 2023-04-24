using Contracts;
using Contracts.Models;
using Core.Commands;

namespace Core.Servisces;
public class CommandService : ICommandService
{
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

}
