using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Models;
using Data.Repositories;

namespace Core;
public class Menu: IMenu
{
    private IUnitOfWork _unitOfWork;

    public Menu(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Menu()
    {
        _unitOfWork = new UnitOfWork();
    }

    public List<MenuCommands> GetAvailableCommands(User user)
    {
        
        var availableCommands = new List<MenuCommands>();

        if (IsCurrentGameExist(user))
        {
            availableCommands.Add(MenuCommands.BackToCurrentGame);
        }

        availableCommands.Add(MenuCommands.NewGame);
        availableCommands.Add(MenuCommands.ConnectToExist);
        availableCommands.Add(MenuCommands.Info);

        return availableCommands;
    }

    public Answer ProcessCommand(InnerParametres innerParametres, MenuCommands command)
    {
        return new Answer();
    }

    public List<Map> GetMapList(InnerParametres innerParametres)
    {
        return new List<Map>();
    }

    //so big function...
    private bool IsCurrentGameExist(User user)
    {
        var playerSessionRepository = _unitOfWork.PlayerSessionRepository;
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
