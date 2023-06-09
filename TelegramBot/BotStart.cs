using Telegram.Bot;
using Telegram.Bot.Polling;
using Core.Services;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Contracts;

namespace TelegramBot
{
    public static class BotStart
    {
        static ITelegramBotClient bot;

        public static void CreateBot(string token)
        {
            bot = new TelegramBotClient(token);
        }

        public static void Start()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITelegramBotClient>(bot);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UnitOfWork>();

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ILobbyService, LobbyService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IBotResultPresenter, BotResultPresenter>();

            services.AddScoped<BotEvents>();

            var serviceProvider = services.BuildServiceProvider();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            var botEvents = serviceProvider.GetRequiredService<BotEvents>();

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
}
