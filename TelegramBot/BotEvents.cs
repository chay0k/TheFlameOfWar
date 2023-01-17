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
    private static IMenu _menuCommands = new Menu();
    public static IMenu Menu { set => _menuCommands = value; }

    public static ITelegramBotClient Bot { get; set; }
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "hello");
        }
        //var displayStatus = false;
        //var currentParameters = new LobbyParametrs();
        //var currentUser = Authorisation.CurrentUser(update, Newtonsoft.Json.JsonConvert.SerializeObject(update));
        //var currentTable = Lobby.SearchActiveTables(currentUser);
        //var currentPlayerCondition =  Lobby.RetrievePlayerCondition(currentUser, currentTable);
        //var currentUserSession = Lobby.RetrieveUserSession(currentUser, currentTable);

        //currentParameters.CurrentUser = currentUser;
        //currentParameters.CurrentPlayerCondition = currentPlayerCondition;
        //currentParameters.CurrentUserSession = currentUserSession;
        //currentParameters.CurrentTable = currentTable;

        //var currentStatus = $"user: \t{currentUser.Name}, {currentUser.TelegramId}: {currentUser.Id}, \n" +
        //    (currentUserSession == null ? "" : $"playerNumber: \t{currentUserSession.PlayerNumber}, {currentUserSession.Location} \n") +
        //    (currentTable == null ? "" : $"currentTable: \t{currentTable.Name}, {currentTable.Token}");



        //if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        //{

        //    var message = update.Message;

        //    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
        //    {
        //        //skip
        //    }
        //    else if (message.Text.ToLower() == "/start")
        //    {
        //        var isExistActiveGame = !(currentTable == null || currentTable.IsEmpty());
        //        await Bot.SendTextMessageAsync(message.Chat.Id, "Make you choice", replyMarkup: BotButtonsProcessing.StartMenu(isExistActiveGame));
        //    }
        //    else if (message.Text.StartsWith("token_"))
        //    {
        //        var sqlTableRepository = new SqlTableRepository();
        //        var table = sqlTableRepository.Get(message.Text);
        //        currentParameters.CurrentTable = Lobby.RetrieveTable(table);
        //        var playerNumber = Lobby.GetNewPlayerNumber(currentParameters.CurrentTable, currentUser);
        //        //?
        //        var CurrentPosition = currentParameters.CurrentUserSession.Location;
        //        if (playerNumber == 0) 
        //            Console.Write("Error!");
        //        else
        //        {
        //            var sqlCellRepository = new SqlCellRepository();
        //            var playerCell = sqlCellRepository.Get(currentParameters.CurrentTable.MapId, playerNumber);
        //            CurrentPosition = BotButtonsProcessing.CalculatePlace(playerCell.CoordinateX, playerCell.CoordinateY);
        //        }
        //        table.Users.Add(currentUser);

        //        currentParameters.CurrentUserSession.Table = currentParameters.CurrentTable;
        //        currentParameters.CurrentUserSession.TableId = currentParameters.CurrentTable.Id;
        //        currentParameters.CurrentUserSession.PlayerNumber = playerNumber;
        //        Lobby.UpdateLobby(currentUser, CurrentPosition, currentParameters.CurrentUserSession);



        //        await Bot.SendTextMessageAsync(message.Chat.Id, $"You are the {currentParameters.CurrentUserSession.PlayerNumber} Player", replyMarkup: BotButtonsProcessing.TableSettings());

        //    }
        //    //else if (message.Text.ToLower() == "/restart")
        //    //{
        //    //    BotButtonsProcessing.CurrentPosition = 1;
        //    //    //MapGenerator.map.Cells[0, 0].IsOpen = true;
        //    //    BotButtonsProcessing.MapButtonAsync(message, currentTable);
        //    //}
        //    if (displayStatus)
        //        await botClient.SendTextMessageAsync(message.Chat, currentStatus);
        //}
        //else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        //{
        //    string codeOfButton = update.CallbackQuery.Data;
        //    BotButtonsProcessing.UpdateStatusAsync(botClient, update, cancellationToken, currentParameters);
        //    if (displayStatus)
        //        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, currentStatus);

        //    //Battlefield.UpdateStatusAsync(botClient, update, cancellationToken, codeOfButton, currentUser, currentTable);
        //}

    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
}

