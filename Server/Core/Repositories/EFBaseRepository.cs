using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EFBaseRepository<T> : BaseRepository, IRepository<T> where T : class {
  protected readonly DbSet<T> _dbSet;

  public EFBaseRepository(DbContext dbContext) : base (dbContext)
  {
    this._dbSet = dbContext.Set<T>();
  }

  public string Name => $"{typeof(T).Name}Repository";

  public void Add(T entity)
  {
    this._dbSet.Add(entity);
  }

  public async Task<T> AddAsync(T entity)
  {
    await this._dbSet.AddAsync(entity, System.Threading.CancellationToken.None);

    return entity;
  }

  public void Delete(T entity)
  {
    this._dbSet.Remove(entity);
  }

  public void Delete(string id)
  {
    var entity = this.FindById(id);

    if (entity != null)
    {
      this._dbSet.Remove(entity);
    }
  }

  public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
  {
    if (predicate == null) {
      throw new ArgumentNullException(nameof(predicate));
    }

    return this._dbSet.Where(predicate).AsEnumerable();
  }

  public IEnumerable<T> FindAll() {
    return this._dbSet.AsEnumerable();
  }

  public async Task<IEnumerable<T>> FindAllAsync()
  {
    return await this._dbSet.ToListAsync();
  }

  public T FindById(string id) {
    return this._dbSet.Find(id);
  }

  public async Task<T> FindByIdAsync(string id) {
    return await this._dbSet.FindAsync(id);
  }

  public void Update(T entity)
  {
    this.DbContext.Entry(entity).State = EntityState.Modified;
    this._dbSet.Attach(entity);
  }
}