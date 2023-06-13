using Contracts.Models;
using Contracts.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services;
public class CellService : ICellService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<CellEntity> _repository;
    //private readonly ILandService _landService;
    //private readonly IMapService _mapService;
    public CellService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        _repository = _unitOfWork.CellRepository;
        //_mapService = serviceProvider.GetRequiredService<IMapService>();
        //_landService = serviceProvider.GetRequiredService<ILandService>();
    }
    public async Task AddCellAsync(Cell cell)
    {
        //_repository.InsertAsync(await CellToCellEntity(cell));
        //_repository.SaveAsync();
    }

    Task ICellService.DeleteCellAsync(Cell cell)
    {
        throw new NotImplementedException();
    }

    Task<List<Lobby>> ICellService.GetAvailableCellsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CellEntity> CellToCellEntity(Cell cell)
    {
        var cellEntity = await _repository.GetByIdAsync(cell.Id);
        if (cellEntity is null)
        {
            cellEntity = new CellEntity
            {
                Id = cell.Id,
                CoordinateX = cell.X,
                CoordinateY = cell.Y,
                //Land = await _landService.LandToLandEntityAsync(cell.Land),
                //Map = await _mapService.MapToMapEntityAsync(cell.Map),
                PlayersStartPosition = cell.PlayerStartPosition
            };
            _repository.InsertAsync(cellEntity);
            return cellEntity;
        }
        else
            return cellEntity;

    }
}
