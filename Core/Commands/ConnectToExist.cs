using Contracts;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class ConnectToExist : ICommand
    {
        private readonly ILobbyService _lobbyService;
        private readonly ICommandService _commandService;

        public ConnectToExist(IServiceProvider serviceProvider)
        {
            _lobbyService = serviceProvider.GetRequiredService<ILobbyService>();
            _commandService = serviceProvider.GetRequiredService<ICommandService>();
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
                    string details = "";
                    if (lobby.Connect(session.SessionPlayer, ref details))
                    {
                        message = "Connected to lobby";
                        _commandService.ExpectedInput = false;
                    }
                    else
                    {
                        message = "Failed connection to lobby";
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
