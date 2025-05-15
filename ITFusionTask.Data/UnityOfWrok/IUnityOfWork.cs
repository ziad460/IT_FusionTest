using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ITFusionTask.Data.UnityOfWrok
{
    public interface IUnityOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

		int Complete();
        Task<int> CompleteAsync();
        Task<IDbContextTransaction> GetContextTransactionAsync();
    }

    public interface IUnityOfWork<TContext> : IUnityOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
