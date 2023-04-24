using Contracts.Models;
using Contracts;
using Core;
using Core.Servisces;
using Data.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static SQLite.SQLite3;


namespace TelegramBot;

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
        _sessionServices = new Dictionary<long, ISessionService>();
        _resultPresenter = resultPresenter;
        _lobbyService = lobbyService;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Message? message = null;
        string text = "";

        if (!IsGetMessageAndText(ref message, ref text, update))
        {
            return;
        }
            
        long chatId = message.Chat.Id;
        if (!_sessionServices.TryGetValue(chatId, out ISessionService sessionService))
        {
            // create new session service for this user if not exist
            sessionService = new SessionService(_playerService, _lobbyService);
            _sessionServices[chatId] = sessionService;
        }
        sessionService.UserTelegramId = chatId;
        sessionService.SessionPlayer = await _playerService.GetPlayerAsync(chatId);

        ICommand? command = null;
        if (text.StartsWith('/'))
            command = _commandService.FindCommand(ref text);
        else if (sessionService.PeekCommand() != null)
            command = sessionService.PeekCommand();

        sessionService.LastInput = text;

        if (command == null)
            _resultPresenter.PresentResult("Unknown command", chatId);
        else
        {
            var result = await command.ExecuteAsync(sessionService);
            _resultPresenter.PresentResult(result, chatId);
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
    private bool IsGetMessageAndText(ref Message message, ref string text, Update update)
    {
        if (update == null)
            return false;
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (update.Message == null)
                return false;
            message = update.Message;
            if (message == null) return false;

            text = message.Text.ToLower();
        }
        else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            if (update.CallbackQuery == null || update.CallbackQuery.Message == null)
                return false;
            message = update.CallbackQuery.Message;
            text = update.CallbackQuery.Data.ToLower();
        }
        if (message == null)
            return false;
        return true;
    }

}

