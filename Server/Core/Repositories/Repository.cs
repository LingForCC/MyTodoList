
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
  {
        public string Name => throw new NotImplementedException();

        public void Add(T entity)
    {
      throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
      throw new NotImplementedException();
    }

    public void Delete(string id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<T> FindAll()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<T> FindById(string id)
    {
      throw new NotImplementedException();
    }

    public void Update(T entity)
    {
      throw new NotImplementedException();
    }

    T IRepository<T>.FindById(string id)
    {
      throw new NotImplementedException();
    }
  }
}