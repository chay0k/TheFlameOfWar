using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Core.Initialization;

namespace TelegramBot;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("test");
        string token = ConfigurationManager.ConnectionStrings["Token"].ConnectionString;
        var i = new Initialization();
        i.InitializeAsync(true);
        BotStart.CreateBot(token);
        BotStart.Start();
    }
}