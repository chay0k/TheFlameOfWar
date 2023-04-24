using Contracts;
using Core.Servisces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class ConnectToExist : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        // Отримати доступні лобі з сесійного сервісу
        var lobbyService  = (ILobbyService)session.GetService(typeof(ILobbyService));

        var availableLobbies = lobbyService.GetAvailableLobbies();

        // Перевірити, чи є доступні лобі
        if (availableLobbies.Count == 0)
        {
            return "No available lobbies to join.";
        }

        // Відобразити доступні лобі користувачу
        var sb = new StringBuilder();
        sb.AppendLine("Available lobbies to join:");
        foreach (var lobby in availableLobbies)
        {
            sb.AppendLine($"- Lobby Id: {lobby.Id}, Name: {lobby.Name}");
        }

        // Повернути рядок-результат
        return sb.ToString();
    }
}

