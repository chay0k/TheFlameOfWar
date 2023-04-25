using Contracts;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class Name : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        string message;
        var playerService = (IPlayerService)session.GetService(typeof(IPlayerService));
        var commandService = (ICommandService)session.GetService(typeof(ICommandService));
        var player = session.SessionPlayer;
        var name = session.LastInput; 
        if (player != null)
        {
            message = $"Your name is '{player.Name}'";
            commandService.ExpectedInput = false;
        }
        else if (name == "") 
        {
            message = "Please enter your name";
            commandService.PushCommand(this, "");
            commandService.ExpectedInput = true;
        }
        else 
        {
            var existingPlayer = await playerService.GetPlayerByNameAsync(name);
            if (existingPlayer != null && existingPlayer.TelegramId == session.UserTelegramId) 
            {
                message = $"Your name is '{name}'";
                commandService.ExpectedInput = false;
            }
            else if (existingPlayer != null)
            {
                message = $"A user with name '{name}' already exists. Please choose another name.";
                commandService.PushCommand(this, "");
                commandService.ExpectedInput = true;
            }
            else
            {             
                player = await playerService.CreateNewAsync(name, session.UserTelegramId);
                session.SessionPlayer = player;
                message = $"User with name '{name}' has been created";
                commandService.ExpectedInput = false;
            }
        }
        return message;
    }
}
