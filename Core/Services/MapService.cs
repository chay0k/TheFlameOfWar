using Contracts.Models;
using Contracts.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services
{
    public class MapService : IMapService
    {
         private readonly ILandService _landService;
        private readonly IResourceService _resourceService;
        private readonly ICellService _cellService;
        private readonly IRepository<MapEntity> _repository;

        public MapService(IServiceProvider serviceProvider)
        {
            _landService = serviceProvider.GetRequiredService<ILandService>();
            _resourceService = serviceProvider.GetRequiredService<IResourceService>();
            _cellService = serviceProvider.GetRequiredService<ICellService>();
            _repository = serviceProvider.GetRequiredService<IUnitOfWork>().MapRepository;
        }

        public async Task<Map> CreateRandomMapAsync()
        {
            int countPlayers = 2;

            var map = new Map();
            map.Id = Guid.NewGuid();
            map.Players = countPlayers;
            var allMaps = await _repository.GetAsync();
            var randomMaps = allMaps.Where(m => m.Name.StartsWith("Random map"));
            map.Name = $"Random map  {randomMaps.Count()}";

            var lands = await _landService.GetAvailableLandsAsync();
            Console.WriteLine("Land list:");
            foreach (var land in lands)
            {
                Console.WriteLine($"{land.Id}.{land.Name} - {land.IsPassable}, {land.Emoji}");
            }
            var landList = lands.ToList();
            var resources = await _resourceService.GetAvailableResourcesAsync();
            Console.WriteLine("Resource list:");
            foreach (var resource in resources)
            {
                Console.WriteLine($"{resource.Id} - {resource.Name} - {resource.Emoji}");
            }
            var resourcesList = resources.ToList();
            var rnd = new Random();
            for (int i = 0; i < map.SizeX; i++)
            {
                for (int j = 0; j < map.SizeY; j++)
                {
                    var randomLandID = rnd.Next(landList.Count);
                    var randomResourceID = rnd.Next(resourcesList.Count);
                    var random10 = rnd.Next(10);
                    var cell = new Cell(i, j, map);
                    Resource? resource = null;
                    var land = landList[randomLandID];
                    cell.Id = Guid.NewGuid();
                    cell.Land = land;
                    if (i == 0 && j == 0)
                        cell.PlayerStartPosition = 1;
                    else if (i == map.SizeX - 1 && j == map.SizeY - 1)
                        cell.PlayerStartPosition = 2;
                    if (random10 == 0 && land.IsPassable)
                    {
                        resource = resourcesList[randomResourceID];
                    }
                    cell.Resource = resource;
                    cell.Land = land;
                    await _cellService.AddCellAsync(cell);
                    map.SetCell(i, j, cell);
                }
            }

            return map;
        }

        public async Task<MapEntity> MapToMapEntityAsync(Map map)
        {
            var mapEntity = await _repository.GetByIdAsync(map.Id);
            if (mapEntity is null)
            {
                mapEntity = new MapEntity
                {
                    Id = map.Id,
                    Name = map.Name,
                    Players = map.Players
                };
                _repository.InsertAsync(mapEntity);
                return mapEntity;
            }
            else
                return mapEntity;
        }
    }
}
