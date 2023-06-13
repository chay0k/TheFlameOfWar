using Contracts.Models;
using Data.Models;

namespace Contracts.Services;
public interface IResourceService
{
    public Task<List<Resource>> GetAvailableResourcesAsync();
}
