using Contracts;
using Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands;
public class Sure : ICommand
{
    private readonly ICommandService _commandService;
    private readonly ILobbyService _lobbyService;
    private readonly ISessionService _sessionService;
    public Sure(IServiceProvider serviceProvider)
    {
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
        _sessionService = serviceProvider.GetRequiredService<ISessionService>();
    }
    public async Task<string> ExecuteAsync()
    {
        var message = "";
        var answer = _sessionService.LastInput;
        if (answer == "n")
        {
            _commandService.ClearCommands();
            message = "succesfully rejected";
            _commandService.ExpectedInput = false;
        }
        else if (answer == "y")
        {
            if(_commandService.PeekCommand().Item1.GetType() == typeof(NewGame))
            {
                message = _lobbyService.DeletePlayerFromLobby(_sessionService.SessionPlayer);
                _commandService.ExpectedInput = false;
            }
        }
        else
        {
            message = "Incorrect command. Send \"Y\" to approve, and \"N\" to reject";
            _commandService.PushCommand(this, "");
            _commandService.ExpectedInput = true;
        }
        return message;
    }
}
