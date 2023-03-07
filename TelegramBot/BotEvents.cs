using Contracts.Models;
using Contracts;
using Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace TelegramBot;

public static class BotEvents
{
    private static IMenuService _menuService = new MenuService();
    public static IMenuService Menu { set => _menuService = value; }
    public static ITelegramBotClient Bot { get; set; }

    public static IUserService _userService = new UserService();
    public static IUserService UserService { set => _userService = value; }
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Message message = null;
        string text = "";
        if (update == null)
            return;
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (update.Message == null)
                return;
            message = update.Message;
            text = message.Text.ToLower();
        }
        else if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            if (update.CallbackQuery == null || update.CallbackQuery.Message == null)
                return;
            message = update.CallbackQuery.Message;
            text = update.CallbackQuery.Data.ToLower();
        }
        if (message == null)
            return;
        var currentUser = _userService.GetUser(message.Chat.Id);
        var userName = "my friend";
        if (currentUser != null)
            userName = currentUser.Name;
        await Bot.SendTextMessageAsync(message.Chat.Id, $"hello, {userName}!");
        if (currentUser == null)
        {
            TemporaryAuthentificationAsync(message, text);
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    private static async Task TemporaryAuthentificationAsync(Message message, string text)
    {
        var command = await CommandServise.FindCommandAsync(text);
        if(command == null)
        {
            await Bot.SendTextMessageAsync(message.Chat.Id, $"I don't understand, please try again");
            return;
        }
        if(!CommandServise.IsAbleToPerform(command, message.Chat.Id))
        {
            await Bot.SendTextMessageAsync(message.Chat.Id, $"You can't do it right now. \nPlease, try another command");
            return;
        }

        var innerParametres = new InnerParametres()
            { User = null, ChatId = message.Chat.Id, Text = text };
        var answer = _menuService.ProcessCommand(innerParametres, command.MenuCommand);
        if (answer != null && answer.IsCompleted)
        {

        }
    }

    private static InnerParametres FillParametres(Contracts.Models.User user, long chatId, string text)
    {
        InnerParametres innerParametres = new InnerParametres()
        { User = user, ChatId = chatId, Text = text };
        return innerParametres;
    }
}

