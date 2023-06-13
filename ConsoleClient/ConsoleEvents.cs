using Core;
using Contracts;
using Contracts.Models;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Contracts.Services;

namespace ConsoleClient;
internal class ConsoleEvents
{
    private readonly ICommandService _commandService;
    private readonly ISessionService _sessionService;
    private readonly IResultPresenter _resultPresenter;
    private readonly IPlayerService _playerService;

    public ConsoleEvents(IServiceProvider serviceProvider)
    {
        _commandService = serviceProvider.GetRequiredService<ICommandService>();
        _playerService = serviceProvider.GetRequiredService<IPlayerService>(); ;
        _sessionService = serviceProvider.GetRequiredService<ISessionService>(); ;
        _resultPresenter = serviceProvider.GetRequiredService<IResultPresenter>();
    }
    public async Task ProceedAsync(string message)
    {
        _commandService.ExpectedInput = false;

        string text = "";

        if (message == "")
        {
            return;
        }

        text = message;

        long chatId = 1;

        _sessionService.UserTelegramId = chatId;
        _sessionService.SessionPlayer = await _playerService.GetPlayerAsync(chatId);

        ICommand? command = null;

        if (text.StartsWith('/'))
        {
            _commandService.ClearCommands();
            command = _commandService.FindCommand(ref text);
            if (command != null)
            {
                _commandService.PushCommand(command, text);
            }
            else
            {
                _resultPresenter.PresentResult("Unknown command");
                return;
            }
        }

        _sessionService.LastInput = text;

        command = _commandService.PopCommand().Item1;
        while (command != null)
        {
            _commandService.ExpectedInput = false;
            string result = await command.ExecuteAsync();
            _resultPresenter.PresentResult(result);

            if (_commandService.ExpectedInput)
            {
                return;
            }

            var (nextCommand, commandArgument) = _commandService.PopCommand();
            command = nextCommand;
            _sessionService.LastInput = commandArgument;
        }
    }


}
