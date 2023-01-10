using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TelegramBot;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("test");
        string token = ConfigurationManager.ConnectionStrings["Token"].ConnectionString;
        BotStart.CreateBot(token);
        BotStart.Start();
    }
}