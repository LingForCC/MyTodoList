using System.Threading.Tasks;
using Core;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EFUnitOfWork : IUnitOfWork
{
  private readonly DbContext _dbContext;

  public EFUnitOfWork(DbContext dbContext) {
    this._dbContext = dbContext;
  }

  public int Complete()
  {
    return _dbContext.SaveChanges();
  }

  public async Task<int> CompleteAsync()
  {
    return await _dbContext.SaveChangesAsync();
  }

  public IRepository<T> GetRepository<T>() where T : class
  {
    throw new System.NotImplementedException();
  }
}
