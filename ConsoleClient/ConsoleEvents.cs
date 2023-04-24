using Core;
using Contracts;
using Contracts.Models;
using Core.Servisces;

namespace ConsoleClient;
internal class ConsoleEvents
{
    private readonly ICommandService _commandService;
    private readonly ISessionService _sessionService;
    private readonly IResultPresenter _resultPresenter;

    public ConsoleEvents(ISessionService sessionService)
    {
        _commandService = new CommandService();
        _sessionService = sessionService;
        _resultPresenter = new ConsoleResultPresenter();
    }
    public ConsoleEvents(ICommandService commandService, ISessionService sessionService, IResultPresenter resultPresenter)
    {
        _commandService = commandService;
        _sessionService = sessionService;
        _resultPresenter = resultPresenter;
    }
    public async Task ProceedAsync(string message)
    {
        var command = _commandService.FindCommand(ref message);
        _sessionService.LastInput = message;
        var result = await command.ExecuteAsync(_sessionService);
        _resultPresenter.PresentResult(result);
    }
}
