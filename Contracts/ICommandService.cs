using Contracts.Models;

namespace Contracts;
public interface ICommandService
{
    public Guid Id { get; set; }
    public bool ExpectedInput { get; set; }
    public ICommand  FindCommand(ref string name);
    public (ICommand, string) PeekCommand();
    public void PushCommand(ICommand command, string message);
    public (ICommand, string) PopCommand();
    public void ClearCommands();
}
