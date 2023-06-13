using Contracts.Models;
using Contracts.Services;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core;
public class GameProcessingService : IGameProcessingService
{
    
    private static int _idCounter = 0;
    private int _id;
    private GameState _state;
    private List<Player> _players;
    private Lobby _board;

    public GameProcessingService()
    {
        _id = _idCounter++;
        _state = GameState.NotStarted;
        _players = new List<Player>();
        _board = null;
    }
    public GameProcessingService(Lobby board)
    {
        _id = _idCounter++;
        _state = GameState.NotStarted;
        _players = new List<Player>();
        _board = board;
    }

    public int Id { get { return _id; } }
    public GameState State { get { return _state; } }
    public List<Player> Players { get { return _players; } }
    public Lobby Board { get { return _board; } }

    public void AddPlayer(Player player)
    {
        if (_state != GameState.NotStarted)
        {
            throw new Exception("Cannot add player after game has started");
        }

        if (_players.Count >= Board.Map.Players)
        {
            throw new Exception("Cannot add more players to the game");
        }

        _players.Add(player);
    }

    public void Start()
    {
        if (_state != GameState.NotStarted)
        {
            throw new Exception("Game has already started");
        }

        if (_players.Count < Board.Map.Players)
        {
            throw new Exception("Not enough players to start the game");
        }

        _state = GameState.InProgress;
    }

    public void End()
    {
        if (_state != GameState.InProgress)
        {
            throw new Exception("Game has not started or has already ended");
        }

        _state = GameState.Finished;
    }
}
