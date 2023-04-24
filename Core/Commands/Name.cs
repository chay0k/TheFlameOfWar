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
        var message = "";
        var playerService = (IPlayerService)session.GetService(typeof(IPlayerService));
        var player = session.SessionPlayer;
        var name = session.LastInput; 
        if (player != null)
        {
            message = $"Your name is '{player.Name}'";
            session.ClearCommands();
        }
        else if (name == "") 
        {
            message = "Please enter your name";
            session.PushCommand(this);
        }
        else 
        {
            var existingPlayer = await playerService.GetPlayerByNameAsync(name);
            if (existingPlayer != null && existingPlayer.TelegramId == session.UserTelegramId) 
            {
                message = $"Your name is '{name}'";
                session.ClearCommands();
            }
            else if (existingPlayer != null)
            {
                message = $"A user with name '{name}' already exists. Please choose another name.";
                session.PushCommand(this);
            }
            else
            {             
                player = await playerService.CreateNewAsync(name, session.UserTelegramId);
                session.SessionPlayer = player;
                message = $"User with name '{name}' has been created";
                session.ClearCommands();
            }
        }
        return message;
    }
}
