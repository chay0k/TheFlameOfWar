using Contracts;
using Contracts.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class Sure : ICommand
{
    private readonly ICommandService _commandService;
    private readonly ILobbyService _lobbyService;
    public Sure(IServiceProvider serviceProvider)
    {
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
    }
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        var message = "";
        var answer = session.LastInput;
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
                message = _lobbyService.DeletePlayerFromLobby(session.SessionPlayer);
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
