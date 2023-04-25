using Contracts;
using Contracts.Models;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace Core.Servisces;
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
    }
    public async Task<Player> GetPlayerAsync(long telegramId)
    {
        var player = await _playerRepository.GetByTelegramIdAsync(telegramId);
        return player != null ? MapToPlayer(player) : null; 
    }
    public async Task<Player> GetPlayerAsync(string telegramId)
    {
        if (!long.TryParse(telegramId, out long id))
        {
            throw new ArgumentException("Invalid telegram id", nameof(telegramId));
        }
        return await GetPlayerAsync(id);
    }
    public async Task<Player> GetPlayerAsync(Guid id)
    {
        var player = await _playerRepository.GetByIdAsync(id);
        return player != null ? MapToPlayer(player) : null;
    }
    public async Task<Player> GetPlayerByNameAsync(string name)
    {
        var player = await _playerRepository.GetByNameAsync(name);
        return player != null ? MapToPlayer(player) : null;
    }
    public async Task<Player> CreateNewAsync(string name, long telegramId = 0)
    {
        var newPlayer = new Player { Name = name, TelegramId = telegramId, FirstName = "", LastName = "" };
        var playerEntity = MapToPlayerEntity(newPlayer);
        await _playerRepository.InsertAsync(playerEntity);
        await _playerRepository.SaveAsync();
        return newPlayer;
    }
    private Player MapToPlayer(PlayerEntity player)
    {
        return player is null ? null : new Player
        {
            Name = player.Name,
            TelegramId = player.TelegramId,
            FirstName = player.FirstName,
            LastName = player.LastName,
            Id = player.Id
        };
    }
    private PlayerEntity MapToPlayerEntity(Player player)
    {
        return player is null ? null : new PlayerEntity
        {
            Id = player.Id,
            Name = player.Name,
            FirstName = player.FirstName,
            LastName = player.LastName,
            TelegramId = player.TelegramId,
        };
    }
}
