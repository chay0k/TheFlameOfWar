using Contracts.Models;
using Contracts;
using Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Core.Services;
using Newtonsoft.Json;

namespace TelegramBot
{
    public class BotEvents
    {
        private readonly ICommandService _commandService;
        private readonly IPlayerService _playerService;
        private readonly ILobbyService _lobbyService;
        private readonly Dictionary<long, ISessionService> _sessionServices;
        private readonly IBotResultPresenter _resultPresenter;

        public BotEvents(ICommandService commandService, IPlayerService playerService, IBotResultPresenter resultPresenter, ILobbyService lobbyService)
        {
            _commandService = commandService;
            _playerService = playerService;
            _resultPresenter = resultPresenter;
            _lobbyService = lobbyService;
            _sessionServices = new Dictionary<long, ISessionService>();
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

            // Отримання або створення сервісу сесії користувача
            if (!_sessionServices.TryGetValue(chatId, out ISessionService sessionService))
            {
                sessionService = new SessionService(_playerService, _lobbyService, _commandService);
                _sessionServices[chatId] = sessionService;
            }
            sessionService.UserTelegramId = chatId;
            sessionService.SessionPlayer = await _playerService.GetPlayerAsync(chatId);

            ICommand? command = null;

            // Пошук команди, якщо текст починається з '/'
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

            // Збереження останнього введеного тексту користувачем
            sessionService.LastInput = text;

            // Виконання команд по черзі, доки очікується введення
            command = _commandService.PopCommand().Item1;
            while (command != null)
            {
                _commandService.ExpectedInput = false;
                var result = await command.ExecuteAsync(sessionService);
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

            if (update == null || message == null)
            {
                return false;
            }

            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    message = update.Message;
                    if (message == null)
                    {
                        return false;
                    }
                    text = message.Text?.ToLowerInvariant() ?? "";
                    break;
                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    var callbackQuery = update.CallbackQuery;
                    if (callbackQuery == null || callbackQuery.Message == null)
                    {
                        return false;
                    }
                    message = callbackQuery.Message;
                    text = callbackQuery.Data?.ToLowerInvariant() ?? "";
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
