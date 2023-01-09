using System.Linq.Expressions;
namespace Data.Repositories;
public interface IRepository<TEntity> : IDisposable
        where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = ""); // получение всех объектов
    Task<TEntity> GetByIdAsync(object id); // получение одного объекта по id
    Task Insert(TEntity entity); // создание объекта
    void Update(TEntity entityToUpdate); // обновление объекта
    Task Delete(object id); // удаление объекта по id
    Task Delete(TEntity entity); // удаление объекта по id
    Task Save();  // сохранение изменений
}
