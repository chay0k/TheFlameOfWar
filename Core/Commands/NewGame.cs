using Contracts;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class NewGame : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        // Створити новий екземпляр Lobby
        var lobby = new Lobby
        {
            Id = Guid.NewGuid().ToString(), // Генерувати новий Id
            Name = "New Lobby", // Встановити назву лобі
            Token = "Some Token", // Встановити токен для лобі
            Map = null, // Встановити карту для гри
            IsActive = false, // Встановити прапорець активності лобі
            Players = new List<Player>() { session.SessionPlayer}, // Ініціалізувати список гравців
        };

        // Додати лобі до сесійного сервісу
        ILobbyService lobbyService = (ILobbyService)session.GetService(typeof(ILobbyService));
        lobbyService.AddLobby(lobby);

        // Повернути рядок-результат
        return "New lobby created. Lobby Id: " + lobby.Id;
    }
}
