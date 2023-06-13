using Contracts;
using Core.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Contracts.Services;

namespace TelegramBot
{
    public class BotEvents
    {
        private readonly ICommandService _commandService;
        private readonly IPlayerService _playerService;
        private readonly ILobbyService _lobbyService;
        private readonly Dictionary<long, ISessionService> _sessionServices;
        private readonly IBotResultPresenter _resultPresenter;
        private readonly IServiceProvider _serviceProvider;

        public BotEvents(IServiceProvider serviceProvider)
        {
            _commandService = serviceProvider.GetRequiredService<ICommandService>(); 
            _playerService = serviceProvider.GetRequiredService<IPlayerService>(); ;
            _resultPresenter = serviceProvider.GetRequiredService<IBotResultPresenter>();
            _lobbyService = serviceProvider.GetRequiredService<ILobbyService>(); 
            _sessionServices = new Dictionary<long, ISessionService>();
            _serviceProvider = serviceProvider;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Очищення стану очікуваного введення перед обробкою нового повідомлення
            _commandService.ExpectedInput = false;

            Message? message = null;
            string text = "";

            if (!TryGetMessageAndText(ref message, ref text, update))
            {
                return;
            }

            long chatId = message.Chat.Id;

            if (!_sessionServices.TryGetValue(chatId, out ISessionService sessionService))
            {
                sessionService = new SessionService(_serviceProvider);
                _sessionServices[chatId] = sessionService;
            }
            sessionService.UserTelegramId = chatId;
            sessionService.SessionPlayer = await _playerService.GetPlayerAsync(chatId);

            ICommand? command = null;

            if (text.StartsWith('/'))
            {
                _commandService.ClearCommands();
                command = _commandService.FindCommand(ref text);
                if (command != null)
                {
                    _commandService.PushCommand(command, text);
                }
                else
                {
                    _resultPresenter.PresentResult("Unknown command", chatId);
                    return;
                }
            }

            sessionService.LastInput = text;

            command = _commandService.PopCommand().Item1;
            while (command != null)
            {
                _commandService.ExpectedInput = false;
                var result = await command.ExecuteAsync();
                _resultPresenter.PresentResult(result, chatId);

                if (_commandService.ExpectedInput)
                {
                    return;
                }

                var (nextCommand, commandArgument) = _commandService.PopCommand();
                command = nextCommand;
                sessionService.LastInput = commandArgument;
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }
        private bool TryGetMessageAndText(ref Message message, ref string text, Update update)
        {
            message = null;
            text = "";

            if (update == null || update.Message == null)
            {
                return false;
            }

            message = update.Message;
            text = message.Text?.ToLowerInvariant() ?? "";

            return true;
        }
    }
}
