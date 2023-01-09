using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Contexts;

namespace Data.Repositories
{
    public class GenericRepository<TEntity>:IRepository<TEntity>, IDisposable
        where TEntity : class

    {
        internal GameContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(GameContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties ="")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {

                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split (new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if(orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            {
                return await query.ToListAsync();
            }
        }
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
        public virtual async Task Delete(object id)
        {
            var entityToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(entityToDelete);
        }
        public virtual Task Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return Task.CompletedTask;
        }
        public virtual void Update (TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task ClearAsync()
        {
            var allEntities = await dbSet.ToListAsync();
            foreach (var entity in allEntities)
                Delete(entity);
        }

        public async Task Save()
        {
            context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
