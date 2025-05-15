using System.Linq.Expressions;

namespace ITFusionTask.Data.UnityOfWrok
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll(string[] includes = null);

        Task<List<T>> GetAllAsync(string[] include = null);
        Task<IQueryable<T>> GetAllAsyncQry(Expression<Func<T, bool>>? expression = null, string[] include = null);

        T AddOne(T entity);

        void AddOneWithNoTracking(T entity);

        void UpdateOne(T entity);

        void DetachOne(T entity);

        T Find(Expression<Func<T, bool>> criteria , string[] includes = null);

        List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        List<T> FindFromSql(FormattableString query);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        IQueryable<T> QueryableFind(Expression<Func<T, bool>> criteria);

        IQueryable<T> QueryableFindWithNoTracking(Expression<Func<T, bool>> criteria);

        void AddList(List<T> entity);

		void RemoveList(List<T> entity);

		void UpdateList(List<T> entity);

        Task<T> FindLastAsync(Expression<Func<T, int>> OrderingColumn, string[] includes = null, Expression<Func<T, bool>>? criteria = null);

        T FindLast(Expression<Func<T, int>> criteria, string[] includes = null);

        Task<int> CountEntity(Expression<Func<T, bool>>? criteria = null);

        Task<List<T>> FindFromSqlAsync(string query);

        //
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression, string[] include = null);

        Task<T>? GetAsync(Expression<Func<T, bool>> expression, string[] include = null);

        Task AddOneAsync(T entity);

        Task UpdateOneAsync(T entity);

        Task DeleteOne(T entity);

        IQueryable<T> QuerableGetAllAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
