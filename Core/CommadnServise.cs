using Data.Models;
using Data.Repositories;

namespace Core;
public static class CommandServise
{
    private static IUnitOfWork _unitOfWork = new UnitOfWork();
    public static IUnitOfWork UnitOfWork { set { _unitOfWork = value; } }
    public static Command FindCommand(string text)
    {
        string name = "";
        if (text.IndexOf(" ") != -1)
        {
            name = text.Split(' ')[0];
        }
        else
        {
            name=text;
        }

        var command = _unitOfWork.CommandRepository.GetAsync().Result.
            Where(x => x.Name == name).FirstOrDefault();
        return command;
    }
    public static bool IsAbleToPerform(Command command, long chatId)
    {
        return true;
    }

    public static async Task<Command> GetLastCommandAsync(long id)
    {
        if (id == 0)
            return null;
        var lastCommandRecord =  await _unitOfWork.LastCommandRepository.GetByIdAsync(id);
        return lastCommandRecord.Command;
    }
    public static async Task UpdateCommandAsync(long chatId, Command command)
    {
        var lastCommandRecord = await _unitOfWork.LastCommandRepository.GetByIdAsync(chatId);
        lastCommandRecord.Command = command;
        _unitOfWork.LastCommandRepository.Update(lastCommandRecord);
        _unitOfWork.LastCommandRepository.SaveAsync();
    }
}
