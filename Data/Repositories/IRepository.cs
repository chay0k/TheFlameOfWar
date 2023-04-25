using System.Linq.Expressions;
namespace Data.Repositories;
public interface IRepository<TEntity> : IDisposable
        where TEntity : class
{
    Task<IQueryable<TEntity>> GetAsync(); 
    Task<TEntity> GetByIdAsync(object id); 
    Task InsertAsync(TEntity entity); 
    void Update(TEntity entityToUpdate); 
    Task DeleteAsync(object id); 
    Task DeleteAsync(TEntity entity); 
    Task SaveAsync();  
}
