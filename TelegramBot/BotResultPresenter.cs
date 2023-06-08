using Contracts;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot;
public class BotResultPresenter : IBotResultPresenter
{
    public ITelegramBotClient Bot { get; set; }
    public BotResultPresenter(ITelegramBotClient bot)
    {
        Bot = bot;
    }
    public void PresentResult(string result, long chatId)
    {
        Bot.SendTextMessageAsync(chatId, result);
    }
}
