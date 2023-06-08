using Contracts;
using Core.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class ConnectToExist : ICommand
    {
        private readonly ILobbyService _lobbyService;
        private readonly ICommandService _commandService;

        public ConnectToExist(ILobbyService lobbyService, ICommandService commandService)
        {
            _lobbyService = lobbyService;
            _commandService = commandService;
        }

        public async Task<string> ExecuteAsync(ISessionService session)
        {
            var message = "";
            var token = session.LastInput;
            if (!string.IsNullOrEmpty(token))
            {
                var lobby = _lobbyService.GetByToken(token);
                if (lobby == null)
                {
                    message = $"There is no lobby with token {token}";
                }
                else
                {
                    if (lobby.Connect(session.SessionPlayer))
                    {
                        message = "Connected to lobby";
                        _commandService.ExpectedInput = false;
                    }
                }
            }

            var availableLobbies = _lobbyService.GetAvailableLobbies();

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
            return message + "\n" + sb.ToString();
        }
    }
}
