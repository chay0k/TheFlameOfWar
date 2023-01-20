using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Models;
using Data.Repositories;
using Data.Models;

namespace Core;
public class MenuService: IMenuService
{
    private IUnitOfWork _unitOfWork;

    public MenuService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public MenuService()
    {
        _unitOfWork = new UnitOfWork();
    }

    private List<MenuCommands> _availableCommands = new List<MenuCommands>();

    public async Task<List<MenuCommands>> GetAvailableCommandsAsync(InnerParametres innerParametres)
    {
        if(innerParametres == null)
            return _availableCommands;

        if (innerParametres.User == null)
        {
            await FillAuthentificationCommandsAsync(innerParametres.ChatId);
            return _availableCommands;
        }

        if (IsCurrentGameExist(innerParametres.User))
        {
            _availableCommands.Add(MenuCommands.BackToCurrentGame);
        }

        _availableCommands.Add(MenuCommands.NewGame);
        _availableCommands.Add(MenuCommands.ConnectToExist);
        _availableCommands.Add(MenuCommands.Info);

        return _availableCommands;
    }

    private async Task FillAuthentificationCommandsAsync(long chatId)
    {
        var lastCommand = await CommandServise.GetLastCommandAsync(chatId);
        if (lastCommand != null && lastCommand.CommandType != CommandTypes.MenuCommand)
            return;

        if (lastCommand == null)
        {
            _availableCommands.Add(MenuCommands.CreateNewUser);
            _availableCommands.Add(MenuCommands.ChooseOtherUser);
        }

        //if (lastCommand.MenuCommand == MenuCommands.CreateNewUser)
        //{
        //    return;
        //}
    }

    public Answer ProcessCommand(InnerParametres innerParametres, MenuCommands command)
    {
        var answer = new Answer();
        switch (command)
        {
            case MenuCommands.CreateNewUser:
                answer = ProcessCreateNewUser(innerParametres);
                break;
        }

        return answer;
    }

    private Answer ProcessCreateNewUser(InnerParametres innerParametres)
    {
        var command = new Command() { CommandType = CommandTypes.MenuCommand, MenuCommand = MenuCommands.CreateNewUser };
        Answer answer = new Answer();
        if (CommandServise.IsAbleToPerform(command, innerParametres.ChatId))
        {
            CommandServise.UpdateCommandAsync(innerParametres.ChatId, command);
            answer.IsCompleted = true;
            answer.Details = "Please write your unic name in format: Name YOUR_UNIC_NAME";
        }
        else
        {
            answer.IsCompleted = false;
        }
        return answer;
    }


    public List<Contracts.Models.Map> GetMapList(InnerParametres innerParametres)
    {
        return new List<Contracts.Models.Map>();
    }

    //so big function...
    private bool IsCurrentGameExist(User user)
    {
        var playerSessionRepository = _unitOfWork.PlayerSessionRepository;
        //ToDo remove nested loops
        var lastPlayerSessions = playerSessionRepository.GetAsync().Result.
            Where(s =>  s.PlayerId == user.Id).
            OrderBy(s => s.DateTime).ToList();

        var lobbiesRepository = _unitOfWork.LobbyRepository;
        var lobbyList = new List<Data.Models.Lobby>();
        foreach (var playerSession in lastPlayerSessions)
        {
            var currentLobby = lobbiesRepository.GetByIdAsync(playerSession.LobbyId).Result;
            if (currentLobby != null && currentLobby.IsOpen)
            {
                lobbyList.Add(currentLobby);
            }
        }

        if (lobbyList.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
