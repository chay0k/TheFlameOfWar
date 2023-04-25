using Data.Repositories;
using Data.Models;
using Contracts.Models;

namespace Core.Initialization;
public class Initialization
{
    private IUnitOfWork _unitOfWork;
    public Initialization(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Initialization()
    {
        _unitOfWork = new UnitOfWork();
    }
    public async Task InitializeAsync(bool clearData = false)
    {
        InitializeStartLands(clearData);
        InitializeStartResourses(clearData);
    }
    private async Task InitializeStartLands(bool clearData = false)
    {
        var landRepository = _unitOfWork.LandRepository;

        if (clearData)
        { 
            await landRepository.ClearAsync();
        }
        else
        {
            var landlist = await landRepository.GetAsync();
            if (landlist.Count() > 0)
            {
                Console.WriteLine("Lands are exist");
                return;
            }
        }
        LandEntity dirt = new LandEntity { Name = "Dirt", IsPassable = true, Emoji = "🏿" };
        LandEntity road = new LandEntity { Name = "Road", IsPassable = true, Emoji = char.ConvertFromUtf32(0x1F6E3) };
        LandEntity mount = new LandEntity { Name = "Mount", IsPassable = false, Emoji = char.ConvertFromUtf32(0x26F0) };

        await landRepository.InsertAsync(dirt);
        await landRepository.InsertAsync(road);
        await landRepository.InsertAsync(mount);
        await landRepository.SaveAsync();
        Console.WriteLine("Lands saved successfully");
    }
    private async Task InitializeStartResourses(bool clearData = false)
    {
        var resourceRepository = _unitOfWork.ResourceRepository;

        if (clearData)
            resourceRepository.ClearAsync();
        else
        {
            var resourcelist = await resourceRepository.GetAsync();
            if (resourcelist.Count() > 0)
            {
                Console.WriteLine("Lands are exist");
                return;
            }
        }

//        emptyResource = new Resource { Name = "Empty", Emoji = "" };
        var money = new ResourceEntity { Name = "Money", Emoji = "💰" };
        var stone = new ResourceEntity { Name = "Stone", Emoji = "🪨" };
        var wood = new ResourceEntity { Name = "Wood", Emoji = "🪵" };
        var steel = new ResourceEntity { Name = "Steel", Emoji = "🔗" };

        //resourceRepository.Create(emptyResource);
        resourceRepository.InsertAsync(money);
        resourceRepository.InsertAsync(stone);
        resourceRepository.InsertAsync(wood);
        resourceRepository.InsertAsync(steel);
        resourceRepository.SaveAsync();
        Console.WriteLine("Resource saved successfully");
    }
}
