using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Initialization;
public class CommandInitialization
{
    private IUnitOfWork _unitOfWork;

    public CommandInitialization(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public CommandInitialization()
    {
        _unitOfWork = new UnitOfWork();
    }

    public async Task InitializeAsync(bool clearPrevious = false)
    {
        var commandRepository = _unitOfWork.CommandRepository;

        if (clearPrevious)
            await commandRepository.ClearAsync();
        else
        {
            var landlist = commandRepository.GetAsync();
            if (landlist != null)
                foreach (var land in landlist)
                {
                    Console.WriteLine("Lands are exist");
                    return;
                }
        }
        Land dirt = new Land { Name = "Dirt", AccessLevel = 1, Passability = Passabilities.Possible, Steps = 4, CardNumber = 1, Emoji = "🏿" };
        Land road = new Land { Name = "Road", AccessLevel = 1, Passability = Passabilities.Possible, Steps = 2, CardNumber = 2, Emoji = char.ConvertFromUtf32(0x1F6E3) };
        Land mount = new Land { Name = "Mount", AccessLevel = 1, Passability = Passabilities.Impossible, CardNumber = 4, Emoji = char.ConvertFromUtf32(0x26F0) };

        landRepository.Create(dirt);
        landRepository.Create(road);
        landRepository.Create(mount);
        landRepository.Save();
        Console.WriteLine("Lands saved successfully");

    }
}
