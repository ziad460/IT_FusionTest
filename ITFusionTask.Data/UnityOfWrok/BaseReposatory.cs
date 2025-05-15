using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ITFusionTask.Data.UnityOfWrok
{
    public class BaseReposatory<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext context;

        public BaseReposatory(DbContext _context)
        {
            context = _context;
            context.Database.SetCommandTimeout(3600);
        }

        public T AddOne(T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }

        public void AddOneWithNoTracking(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.FirstOrDefault(criteria);
        }

        public T FindLast(Expression<Func<T, int>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.OrderByDescending(criteria).FirstOrDefault();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<T> FindLastAsync(Expression<Func<T, int>> OrderingColumn, string[] includes = null, Expression<Func<T, bool>>? criteria = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            if (criteria == null)
                return await query.OrderByDescending(OrderingColumn).FirstOrDefaultAsync();

            return await query.Where(criteria).OrderByDescending(OrderingColumn).FirstOrDefaultAsync();
        }

        public async Task<int> CountEntity(Expression<Func<T, bool>>? criteria = null)
        {
            IQueryable<T> query = context.Set<T>();
            if(criteria != null)
                return query.Where(criteria).Count();
            return query.Count();
        }

        public List<T> FindFromSql(FormattableString query)
        {
            IQueryable<T> result = context.Set<T>().FromSql(query);

            return result.ToList();
        }

        public async Task<List<T>> FindFromSqlAsync(string query)
        {
            IQueryable<T> result = context.Set<T>().FromSqlRaw(query);

            return await result.ToListAsync();
        }

        public List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.Where(criteria).ToList();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return await query.Where(criteria).ToListAsync();
        }

        public List<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public async Task<List<T>> GetAllAsync(string[] include = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (include != null)
            {
                foreach (var item in include)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            var x = await query.ToListAsync();
            return x;
        }

        public IQueryable<T> QuerableGetAllAsync()
        {
            return context.Set<T>().AsNoTracking();
        }

        public void UpdateOne(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void DetachOne(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<T> QueryableFind(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().Where(criteria);
        }

        public IQueryable<T> QueryableFindWithNoTracking(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().AsNoTracking().Where(criteria);
        }

        public void AddList(List<T> entity)
		{
			context.Set<T>().AddRange(entity);
		}

		public void RemoveList(List<T> entity)
		{
			context.Set<T>().RemoveRange(entity);
		}

		public void UpdateList(List<T> entity)
		{
			context.Set<T>().UpdateRange(entity);
		}

        public async Task AddOneAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public async Task UpdateOneAsync(T entity)
        {
            //context.Set<T>().Update(entity);
            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression, string[] include = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (include != null)
            {
                foreach (var item in include)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            if (expression != null)
            {
                return await query.Where(expression).ToListAsync();
            }
            var x = await query.ToListAsync();
            return x;
        }

        public async Task<T>? GetAsync(Expression<Func<T, bool>> expression, string[] include = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (include != null)
            {
                foreach (var item in include)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IQueryable<T>> GetAllAsyncQry(Expression<Func<T, bool>>? expression = null, string[] include = null)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (include != null)
            {
                foreach (var item in include)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async Task DeleteOne(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().AnyAsync(predicate);
        }
    }
}
