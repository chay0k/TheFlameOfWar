using Contracts;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class Sure : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        var commandService = (ICommandService)session.GetService(typeof(ICommandService));

        var message = "";
        var answer = session.LastInput;
        if (answer == "n")
        {
            commandService.ClearCommands();
            message = "succesfully rejected";
            commandService.ExpectedInput = false;
        }
        else if (answer == "y")
        {
            //if (commandService.PeekCommand().GetType() == typeof(Sure))
            //    commandService.PopCommand();
            if(commandService.PeekCommand().Item1.GetType() == typeof(NewGame))
            {
                var lobbyService = (ILobbyService)session.GetService(typeof(ILobbyService));
                message = lobbyService.DeletePlayerFromLobby(session.SessionPlayer);
                commandService.ExpectedInput = false;
                //message = await commandService.PopCommand().ExecuteAsync(session);
            }
        }
        else
        {
            message = "Incorrect command. Send \"Y\" to approve, and \"N\" to reject";
            commandService.PushCommand(this, "");
            commandService.ExpectedInput = true;
        }
        return message;
    }
}
