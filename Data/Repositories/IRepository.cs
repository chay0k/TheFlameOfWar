using System.Linq.Expressions;
namespace Data.Repositories;
public interface IRepository<TEntity> : IDisposable
        where TEntity : class
{
    Task<IQueryable<TEntity>> GetAsync(); 
    Task<TEntity> GetByIdAsync(object id); 
    Task InsertAsync(TEntity entity); 
    void Update(TEntity entityToUpdate); // обновление объекта
    Task DeleteAsync(object id); // удаление объекта по id
    Task DeleteAsync(TEntity entity); // удаление объекта по id
    Task SaveAsync();  // сохранение изменений
}
