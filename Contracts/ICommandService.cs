using Contracts.Models;

namespace Contracts;
public interface ICommandService
{
    public ICommand FindCommand(ref string name);
}
