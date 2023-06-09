using Core;
using Contracts;
using Contracts.Models;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

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
        var command = _commandService.FindCommand(ref message);
        _sessionService.LastInput = message;
        var result = await command.ExecuteAsync(_sessionService);
        _resultPresenter.PresentResult(result);
    }
}
