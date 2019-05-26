using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {

        public int Complete()
        {
            // nothing to do.
            return 1;
        }

        public Task<int> CompleteAsync()
        {
            // nothing to do.
            return System.Threading.Tasks.Task.FromResult(1);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new InMemoryRepository<T>();
        }
    }

    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private static readonly List<T> _store = new List<T>();

        public string Name => throw new NotImplementedException();

        public void Add(T entity)
        {
            _store.Add(entity);
        }

        public void Delete(T entity)
        {
            _store.Remove(entity);
        }

        public void Delete(string id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                _store.Remove(entity);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new NullReferenceException(nameof(predicate));
            }

            return _store.AsQueryable().Where(predicate).AsEnumerable();
        }

        public T FindById(string id)
        {
            return _store.Find(it => ((dynamic)it).Id == id);
        }

        public IEnumerable<T> FindAll()
        {
            return _store;
        }

        public void Update(T entity)
        {
            // nothing to do.
        }

    public Task<T> FindByIdAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
      throw new NotImplementedException();
    }
  }
}