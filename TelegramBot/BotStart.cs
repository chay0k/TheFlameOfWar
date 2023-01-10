using System.Configuration;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using System.Configuration;

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
        Console.WriteLine(bot.GetMeAsync().Result.FirstName + " was started");

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        BotEvents.Bot = bot;
        bot.StartReceiving(
            BotEvents.HandleUpdateAsync,
            BotEvents.HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}