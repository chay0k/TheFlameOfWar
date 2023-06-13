using Contracts.Models;

namespace Contracts.Services;
public interface ICellService
{
    public Task AddCellAsync(Cell cell);
    public Task DeleteCellAsync(Cell cell);
    public Task<List<Lobby>> GetAvailableCellsAsync();
}
