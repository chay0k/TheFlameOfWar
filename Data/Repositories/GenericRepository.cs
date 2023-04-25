using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Contexts;

namespace Data.Repositories
{
    public class GenericRepository<TEntity>:IRepository<TEntity>, IDisposable
        where TEntity : class

    {
        internal GameDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(GameDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IQueryable<TEntity>> GetAsync()
        {
            IQueryable<TEntity> query = dbSet;

            return query.AsQueryable();
        }
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(entityToDelete);
        }
        public virtual Task DeleteAsync(TEntity entityToDelete)
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
                DeleteAsync(entity);
        }

        public async Task SaveAsync()
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
