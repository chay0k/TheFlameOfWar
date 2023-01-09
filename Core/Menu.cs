using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Models;
using Data.Repositories;

namespace Core;
public class Menu: IMenuProcessing
{
    private UnitOfWork unitOfWork = new UnitOfWork();

    public List<MenuCommands> GetAvailableCommands(InnerParametres innerParametres)
    {
        
        var availableCommands = new List<MenuCommands>();

        if (IsCurrentGameExist(innerParametres))
        {
            availableCommands.Add(MenuCommands.BackToCurrentGame);
        }

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

    private bool IsCurrentGameExist(InnerParametres innerParametres)
    {
        var playerSessionRepository = unitOfWork.PlayerSessionRepository;
        var lastPlayerSessions = playerSessionRepository.GetAsync(
            s => s.PlayerId == innerParametres.User.Id,
            q => q.OrderByDescending(s => s.DateTime)).Result;

        var lobbiesRepository = unitOfWork.LobbyRepository;
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
