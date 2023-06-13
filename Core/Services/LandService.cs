using Contracts.Models;
using Contracts.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;
public class LandService : ILandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<LandEntity> _repository;
    public LandService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        _repository = _unitOfWork.LandRepository;
    }
    public async Task<List<Land>> GetAvailableLandsAsync()
    {
        var landList = await _repository.GetAsync();
        return await LandEntitiesToLand(landList);
    }

    public async Task<List<Land>> LandEntitiesToLand(IQueryable<LandEntity> landList)
    {
        var lands = await landList.Select(entity => new Land
        {
            Id = entity.Id,
            Name = entity.Name,
            IsPassable = entity.IsPassable,
            Emoji = entity.Emoji
        }).ToListAsync();

        return lands;
    }

    public async Task <LandEntity> LandToLandEntityAsync(Land land)
    {
        var landEntity = await _repository.GetByIdAsync(land.Id);
        if (landEntity is null)
        {
            landEntity = new LandEntity
            {
                Id = land.Id,
                Name = land.Name,
                IsPassable = land.IsPassable,
                Emoji = land.Emoji
            };
            _repository.InsertAsync(landEntity);
            return landEntity;
        }
        else
            return landEntity;
    }

}
