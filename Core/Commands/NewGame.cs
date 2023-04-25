using Contracts;
using Contracts.Models;
using Core.Commands;
using Core.Servisces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class NewGame : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        var commandService = (ICommandService)session.GetService(typeof(ICommandService));

        var message ="";
        var player = session.SessionPlayer;
        var lobbyName = session.LastInput;
        if (player == null)
        {
            message = "Please enter your name before start";
            commandService.PushCommand(this, lobbyName);
            commandService.PushCommand(new Name(), "");
            commandService.ExpectedInput = true;
            return message;
        }

        var lobbyService = (ILobbyService)session.GetService(typeof(ILobbyService));

        //lobbyService.GetAvailableLobbies();
        var playerLobby = lobbyService.GetByPlayer(player);
        if (lobbyName == "")
        {
            message = "Please enter lobby name";
            commandService.PushCommand(this, "");
            commandService.ExpectedInput = true;
        }
        else if (playerLobby != null)
        {
            message = $"You take part in lobby \"{playerLobby.Name}\". \nTo create a new lobby, you will be excluded from the previous one. \nDo you agree with this?";
            commandService.PushCommand(this, lobbyName);
            commandService.PushCommand(new Sure(), "");
            commandService.ExpectedInput = true;
        }
        else
        {
            var lobby = new Lobby
            {
                Id = Guid.NewGuid().ToString(), // Генерувати новий Id
                Name = lobbyName, // Встановити назву лобі
                Token = await TokenGenerator.GetRandomWord(lobbyName),
                Map = null, // Встановити карту для гри
                IsActive = true, // Встановити прапорець активності лобі
                Players = new List<Player>() { player }, // Ініціалізувати список гравців
            };
            lobbyService.AddLobby(lobby);
            message = $"Lobby \"{lobbyName}\" token: \"{lobby.Token}\" created";
            commandService.ExpectedInput = false;
        }
        // Повернути рядок-результат
        return message;
    }
}
