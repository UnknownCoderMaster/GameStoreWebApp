using GameStoreWebApp.Data.Contexts;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	public readonly GameStoreDbContext dbContext;
	public readonly DbSet<T> dbSet;

    public GenericRepository(GameStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
		this.dbSet = dbContext.Set<T>();
    }
    public async ValueTask<T> CreateAsync(T entity) =>
		(await dbContext.AddAsync(entity)).Entity;

	public async ValueTask<bool> DeleteAsync(int id)
	{
		var entity = await GetAsync(e => e.Id == id);

		if (entity == null)
			return false;

		dbSet.Remove(entity);

		return true;
	}

	public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true)
	{
		var query = expression is null ? dbSet : dbSet.Where(expression);

		if (includes != null)
			foreach (var include in includes)
				if (!string.IsNullOrEmpty(include))
					query = query.Include(include);

		if (!isTracking)
			query = query.AsNoTracking();

		return query;
	}

	public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null) =>
		await GetAll(expression, includes, false).FirstOrDefaultAsync();

	public T Update(T entity) =>
		dbSet.Update(entity).Entity;

	public async ValueTask SaveChangesAsync() =>
		await dbContext.SaveChangesAsync();
}
