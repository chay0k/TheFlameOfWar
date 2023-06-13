using Contracts.Models;
using Data.Models;

namespace Contracts.Services;
public interface ILandService
{
    public Task<List<Land>> GetAvailableLandsAsync();
    public Task<LandEntity> LandToLandEntityAsync(Land land);
}
