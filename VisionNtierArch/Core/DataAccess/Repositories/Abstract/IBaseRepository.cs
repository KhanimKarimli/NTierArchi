
namespace Core.DataAccess.Repositories.Abstract
{
	public interface IBaseRepository<TEntity>
		where TEntity : class,IEntity, new()
	{
		Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes);
		Task<List<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes);
		Task AddAsync(TEntity entity);
		void Delete(TEntity entity);
		void Update(TEntity entity);

	}
}
