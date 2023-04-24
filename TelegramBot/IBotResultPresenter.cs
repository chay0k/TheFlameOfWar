using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
namespace Telegram.Bot;
public interface IBotResultPresenter
{
    public ITelegramBotClient Bot { get; set; }
    void PresentResult(string result, long chatId);
}
