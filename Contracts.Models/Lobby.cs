namespace Contracts.Models;
public class Lobby
{
    public string Id { get; set; }
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";
    public Map Map { get; set; }
    public bool IsActive { get; set; }
    public bool IsHotSeat { get; set; }
    public List<Player> Players { get; set; }
    public async Task StartNewGameAsync()
    {
        // Логіка для створення нової гри
        // Включаючи вибір карти, запрошення друзів, обрання режиму гри
        // "по черзі з одного пристрою" та початок гри
    }

    public void ChooseMap(Map selectedMap)
    {
        // Логіка для вибору карти
        Map = selectedMap;
    }

    public bool Connect(Player player, ref string details)
    {
        if (IsHotSeat)
        {
            details = "Can't connect to hotseat lobby.";
            return false;
        }
        else if (Map is null)
        {
            details = "Map is empty. Please create or choose the map.";
            return false;
        }
        else if (Map.Players >= Players.Count)
        {
            details = "The lobby is full.";
            return false;
        }
        else if (Players.Contains(player))
        {
            details = "You are currently in this lobby.";
            return false;
        }
        else
        {
            Players.Add(player);
            return true;
        }

    }

    public void ChangeGameMode()
    {
        IsHotSeat = !IsHotSeat;
    }

    public void StartGame()
    {
        // Логіка для початку гри
    }

}

