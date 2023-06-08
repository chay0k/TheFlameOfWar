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

    public bool Connect(Player player)
    {
        if (
            IsHotSeat ||
            Map.Players >= Players.Count ||
            Players.Contains(player)
            ) return false;
        Players.Add(player);
        return true;
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

