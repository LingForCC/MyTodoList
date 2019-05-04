using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Repositories
{
  public interface IRepository<T> where T : class
  {
      T FindById(string id);

      IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

      IEnumerable<T> FindAll();
      
      void Add(T entity);

      void Delete(T entity);

      void Delete(string id);

      void Update(T entity);
  }
}