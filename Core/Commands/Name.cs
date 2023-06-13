using Contracts;
using Contracts.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands;
public class Name : ICommand
{
    private readonly ICommandService _commandService;
    private readonly IPlayerService _playerService;
    private readonly ISessionService _sessionService;
    public Name(IServiceProvider serviceProvider)
    {
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
        _playerService = serviceProvider.GetRequiredService<IPlayerService>();
        _sessionService = serviceProvider.GetRequiredService<ISessionService>();
    }
    public async Task<string> ExecuteAsync()
    {
        var player = _sessionService.SessionPlayer;
        var name = _sessionService.LastInput;

        // Перевірка чи вже задано ім'я гравця в сесії, якщо так, то повернути повідомлення
        if (player != null)
        {
            return $"Your name is '{player.Name}'";
        }

        // Перевірка чи введено ім'я гравця, якщо ні, то повернути повідомлення та запропонувати ввести
        if (string.IsNullOrEmpty(name))
        {
            return RequestName();
        }

        // Перевірка чи існує гравець з таким ім'ям у базі даних
        var existingPlayer = await _playerService.GetPlayerByNameAsync(name);

        // Якщо гравець з таким ім'ям існує, то перевірити чи це той самий гравець, який зараз користується ботом
        if (existingPlayer != null && existingPlayer.TelegramId == _sessionService.UserTelegramId)
        {
            return $"Your name is '{name}'";
        }

        // Якщо гравець з таким ім'ям вже існує, то запропонувати ввести інше ім'я
        if (existingPlayer != null)
        {
            return RequestName($"A user with name '{name}' already exists. Please choose another name.");
        }

        // Створення нового гравця з введеним іменем
        player = await _playerService.CreateNewAsync(name, _sessionService.UserTelegramId);
        _sessionService.SessionPlayer = player;
        return $"User with name '{name}' has been created";
    }
    private string RequestName(string errorMessage = null)
    {
        _commandService.PushCommand(this, "");
        _commandService.ExpectedInput = true;
        return errorMessage ?? "Please enter your name";
    }
}
