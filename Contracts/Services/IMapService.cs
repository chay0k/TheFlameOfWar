using Contracts.Models;
using Data.Models;

namespace Contracts.Services;
public interface IMapService
{
    public Task<Map> CreateRandomMapAsync();
    public Task<MapEntity> MapToMapEntityAsync(Map map);
}
