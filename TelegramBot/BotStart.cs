using System.Configuration;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using System.Configuration;
using Core.Services;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Core;

namespace TelegramBot;
public static class BotStart
{
    static ITelegramBotClient bot;

    public static void CreateBot(string token)
    {
        bot = new TelegramBotClient(token);
    }
    public static void Start()
    {
        var unitOfWork              = new UnitOfWork();
        var playerRepository        = new PlayerRepository(unitOfWork);
        var mapService              = new MapService(unitOfWork);    
        var playerService           = new PlayerService(playerRepository);
        var lobbyService            = new LobbyService();
        var commandService          = new CommandService(lobbyService, playerService, mapService);
        var botResultPresenter      = new BotResultPresenter(bot);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        var botEvents = new BotEvents(commandService, playerService, botResultPresenter, lobbyService);
        bot.StartReceiving(
            botEvents.HandleUpdateAsync,
            BotEvents.HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.WriteLine(bot.GetMeAsync().Result.FirstName + " was started");
        Console.ReadLine();
    }
}