using Contracts.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services;
public class ResourceService : IResourceService
{
    private readonly IRepository<ResourceEntity> _repository;
    public ResourceService(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.ResourceRepository;
    }
    public async Task<List<Resource>> GetAvailableResourcesAsync()
    {
        var resourceList = await _repository.GetAsync();
        return await ResourceEntityToResource(resourceList);
    }

    private async Task<List<Resource>> ResourceEntityToResource(IQueryable<ResourceEntity> resourceList)
    {
        var resources = await resourceList.Select(entity => new Resource
        {
            Id = entity.Id,
            Name = entity.Name,
            Emoji = entity.Emoji
        }).ToListAsync();

        return resources;
    }
}
